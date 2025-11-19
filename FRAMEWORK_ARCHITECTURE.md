# 🏗️ Ejderya Framework - Mimari Yapı ve Dokümantasyon

**Ejderya Framework**, .NET Framework 4.5+ ile geliştirilmiş, **N-Katmanlı (N-Layer) Mimari**, **SOLID prensipleri** ve **OOP** yaklaşımlarına uygun bir enterprise framework'tür.

---

## 📦 Katmanlar ve Sorumlulukları

### 1. EjderyaFramework.Core (Çekirdek Katman)

Framework'ün temel altyapısını sağlar. Tüm katmanlar tarafından kullanılabilir.

#### İçeriği:

**Entities:**
- `IEntity` interface'i - tüm entity'lerin implement etmesi gereken temel arayüz

**DataAccess:**
- `IEntityRepository<T>` - Generic repository pattern
- **EntityFramework** ve **NHibernate** için base repository implementasyonları
- `EfEntityRepositoryBase`, `NhEntityRepositoryBase`

**Aspects (PostSharp ile AOP):**
- `CacheAspect` - Caching
- `LogAspect` - Loglama
- `ValidationAspect` - Validasyon
- `TransactionScopeAspect` - Transaction yönetimi
- `PerformanceCounterAspect` - Performans ölçümü
- `SecuredOperation` - Yetkilendirme
- `ExceptionLogAspect` - Hata loglama

**CrossCuttingConcerns:**
- **Caching**: `MemoryCacheManager`
- **Logging**: Log4Net entegrasyonu
- **Validation**: FluentValidation entegrasyonu
- **Security**: Authentication ve Authorization helper'ları
- **Utilities**: AutoMapper, WCF Proxy, MVC Infrastructure

---

### 2. EjderyaFramework.Entities (Varlık Katmanı)

Veritabanı tablolarını temsil eden entity sınıfları.

#### İçeriği:

**Concrete:**
- `Product` - Ürün entity'si
- `Category` - Kategori entity'si
- `User` - Kullanıcı entity'si
- `Role` - Rol entity'si
- `UserRole` - Kullanıcı-Rol ilişkisi

**ComplexTypes:**
- `ProductDetail` - Ürün detay DTO'su
- `UserRoleItem` - Kullanıcı rol DTO'su

#### Örnek Entity:

```csharp
public class Product : IEntity
{
    public virtual int ProductId { get; set; }
    public virtual int CategoryId { get; set; }
    public virtual string ProductName { get; set; }
    public virtual decimal UnitPrice { get; set; }
    public virtual short UnitsInStock { get; set; }
    public virtual string QuantityPerUnit { get; set; }
}
```

---

### 3. EjderyaFramework.DataAccess (Veri Erişim Katmanı)

Veritabanı işlemlerini yönetir. **Repository Pattern** kullanır.

#### İçeriği:

**Abstract:**
- `IProductDal` - Ürün repository interface'i
- `ICategoryDal` - Kategori repository interface'i
- `IUserDal` - Kullanıcı repository interface'i

**Concrete:**
- **EntityFramework**: `EfProductDal`, `NorthwindContext` (DbContext)
- **NHibernate**: NHibernate implementasyonları
- **Mappings**: Entity-Table mapping'leri

#### Örnek Repository:

```csharp
public interface IProductDal : IEntityRepository<Product>
{
    List<ProductDetail> GetProductDetails();
}

public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
{
    public List<ProductDetail> GetProductDetails()
    {
        using (var context = new NorthwindContext())
        {
            var result = from p in context.Products
                         join c in context.Categories on p.CategoryId equals c.CategoryId
                         select new ProductDetail
                         {
                             ProductId = p.ProductId,
                             ProductName = p.ProductName,
                             CategoryName = c.CategoryName,
                             UnitPrice = p.UnitPrice
                         };
            return result.ToList();
        }
    }
}
```

---

### 4. EjderyaFramework.Business (İş Mantığı Katmanı)

İş kurallarını ve validasyonları içerir.

#### İçeriği:

**Abstract:**
- `IProductService` - Ürün servis interface'i (WCF için ServiceContract)
- `IUserService` - Kullanıcı servis interface'i

**Concrete/Manager:**
- `ProductManager` - Ürün servis implementasyonu
- `UserManager` - Kullanıcı servis implementasyonu

**ValidationRules/FluentValidation:**
- `ProductValidator` - Ürün validasyon kuralları

**DependencyResolvers/Ninject:**
- `BusinessModule` - Dependency Injection yapılandırması
- `AutoMapperModule` - AutoMapper yapılandırması
- `ValidationModule` - Validation yapılandırması

**Mappings:**
- AutoMapper profilleri

**ServiceContracts:**
- WCF service contract'ları

#### Örnek Service:

```csharp
public class ProductManager : IProductService
{
    private IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    [CacheAspect(typeof(MemoryCacheManager))]
    [PerformanceCounterAspect(2)]
    public List<Product> GetAll()
    {
        return _productDal.GetList();
    }

    [FluentValidationAspect(typeof(ProductValidator))]
    [TransactionScopeAspect]
    public Product Add(Product product)
    {
        return _productDal.Add(product);
    }

    [FluentValidationAspect(typeof(ProductValidator))]
    [TransactionScopeAspect]
    public Product Update(Product product)
    {
        return _productDal.Update(product);
    }

    [TransactionScopeAspect]
    public void Delete(Product product)
    {
        _productDal.Delete(product);
    }
}
```

#### Örnek Validator:

```csharp
public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty().WithMessage("Ürün adı boş olamaz");
        RuleFor(p => p.UnitPrice).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalıdır");
        RuleFor(p => p.UnitsInStock).GreaterThanOrEqualTo((short)0).WithMessage("Stok negatif olamaz");
    }
}
```

---

### 5. EjderyaFramework.MvcWebUI (Sunum Katmanı - MVC)

ASP.NET MVC ile geliştirilmiş web arayüzü.

#### İçeriği:

**Controllers:**
- `ProductController` - Ürün işlemleri
- `AccountController` - Kullanıcı işlemleri

**Views:**
- Razor view'ları

**Models:**
- ViewModel'ler

**App_Start:**
- Route yapılandırması
- Bundle yapılandırması
- Filter yapılandırması

---

### 6. EjderyaFramework.WebApi (Web API Katmanı)

RESTful API servisleri.

#### İçeriği:

**Controllers:**
- API Controller'ları

**MessageHandlers:**
- HTTP message handler'ları (authentication, logging vb.)

---

### 7. EjderyaFramework.Wcf.Service (WCF Servis Katmanı)

SOAP tabanlı web servisleri.

---

### 8. EjderyaFramework.MvcAngularUI (Angular Entegrasyonu)

Angular ile geliştirilmiş modern SPA arayüzü.

---

### 9. Test Projeleri

- `EjderyaFramework.Business.Tests` - İş katmanı testleri
- `EjderyaFramework.DataAccess.Tests` - Veri erişim testleri
- `EjderyaFramework.MvcWebUI.Tests` - MVC testleri

---

## 🎯 Kullanılan Teknolojiler ve Pattern'ler

### Mimari Pattern'ler:
✅ **N-Layer Architecture** (Katmanlı Mimari)
✅ **Repository Pattern** (Veri erişim soyutlaması)
✅ **Dependency Injection** (Ninject)
✅ **AOP** (Aspect Oriented Programming - PostSharp)

### Teknolojiler:
✅ **ORM**: Entity Framework & NHibernate
✅ **Validation**: FluentValidation
✅ **Mapping**: AutoMapper
✅ **Caching**: MemoryCache
✅ **Logging**: Log4Net
✅ **Unit Testing**: Test projeleri
✅ **WCF & Web API**: Servis katmanları

---

## 🔄 Veri Akışı

```
┌─────────────────────────────────────┐
│  UI Layer                           │
│  (MVC/WebAPI/WCF/Angular)          │
└──────────────┬──────────────────────┘
               │
               ▼
┌─────────────────────────────────────┐
│  Business Layer                     │
│  (Manager Classes + Aspects)        │
│  - Validation                       │
│  - Caching                          │
│  - Logging                          │
│  - Transaction                      │
└──────────────┬──────────────────────┘
               │
               ▼
┌─────────────────────────────────────┐
│  Data Access Layer                  │
│  (Repository Pattern)               │
│  - IEntityRepository<T>             │
│  - EfEntityRepositoryBase           │
└──────────────┬──────────────────────┘
               │
               ▼
┌─────────────────────────────────────┐
│  Database                           │
│  (Entity Framework/NHibernate)      │
└─────────────────────────────────────┘
```

---

## 🔧 Dependency Injection Yapılandırması

Framework, **Ninject** kullanarak dependency injection sağlar:

```csharp
public class BusinessModule : NinjectModule
{
    public override void Load()
    {
        Bind<IProductService>().To<ProductManager>().InSingletonScope();
        Bind<IProductDal>().To<EfProductDal>().InSingletonScope();
        
        Bind<ICategoryService>().To<CategoryManager>().InSingletonScope();
        Bind<ICategoryDal>().To<EfCategoryDal>().InSingletonScope();
        
        Bind<IUserService>().To<UserManager>().InSingletonScope();
        Bind<IUserDal>().To<EfUserDal>().InSingletonScope();
    }
}
```

---

## 🎭 Aspect Oriented Programming (AOP)

Framework, **PostSharp** kullanarak cross-cutting concerns'leri yönetir:

### Kullanılabilir Aspect'ler:

1. **CacheAspect** - Metod sonuçlarını cache'ler
2. **LogAspect** - Metod çağrılarını loglar
3. **ValidationAspect** - FluentValidation ile validasyon yapar
4. **TransactionScopeAspect** - Transaction yönetimi sağlar
5. **PerformanceCounterAspect** - Performans ölçümü yapar
6. **SecuredOperation** - Yetkilendirme kontrolü yapar
7. **ExceptionLogAspect** - Hataları loglar

### Kullanım Örneği:

```csharp
[CacheAspect(typeof(MemoryCacheManager))]
[PerformanceCounterAspect(2)]
[LogAspect(typeof(DatabaseLogger))]
public List<Product> GetAll()
{
    return _productDal.GetList();
}
```

---

## 📝 SOLID Prensipleri

Framework, SOLID prensiplerine uygun olarak tasarlanmıştır:

- **S**ingle Responsibility: Her sınıf tek bir sorumluluğa sahip
- **O**pen/Closed: Extension'a açık, modification'a kapalı
- **L**iskov Substitution: Alt sınıflar üst sınıfların yerine kullanılabilir
- **I**nterface Segregation: Interface'ler küçük ve spesifik
- **D**ependency Inversion: Yüksek seviye modüller düşük seviye modüllere bağımlı değil

---

## 🚀 Kullanım Senaryoları

### Yeni Bir Entity Ekleme:

1. `EjderyaFramework.Entities` katmanında entity sınıfı oluştur
2. `EjderyaFramework.DataAccess/Abstract` içinde repository interface'i oluştur
3. `EjderyaFramework.DataAccess/Concrete/EntityFramework` içinde repository implementasyonu yap
4. `EjderyaFramework.Business/Abstract` içinde service interface'i oluştur
5. `EjderyaFramework.Business/Concrete/Manager` içinde service implementasyonu yap
6. `EjderyaFramework.Business/ValidationRules` içinde validator oluştur
7. Ninject module'üne binding'leri ekle

### Yeni Bir Aspect Ekleme:

1. `EjderyaFramework.Core/Aspects` içinde yeni aspect sınıfı oluştur
2. `MethodInterceptionAspect` veya uygun base aspect'ten türet
3. `OnBefore`, `OnAfter`, `OnException` metodlarını override et
4. İlgili servislerde attribute olarak kullan

---

## 📚 Ek Kaynaklar

- **Repository Pattern**: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
- **Dependency Injection**: http://www.ninject.org/
- **AOP with PostSharp**: https://www.postsharp.net/
- **FluentValidation**: https://fluentvalidation.net/
- **AutoMapper**: https://automapper.org/

---

## 📄 Lisans

Bu framework, enterprise projeler için geliştirilmiş bir örnek framework'tür.

---

**Son Güncelleme:** 19 Kasım 2025
