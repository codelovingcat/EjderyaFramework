using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjderyaFramework.DataAccess.Concrete.EntityFramework.Mappings
{
    /// <summary>
    /// Mapping işlemleri veritabanı tabloları ile bizim nesnelerimizin bağlantısını sağlar...
    /// Şuanki dururmda buna ihtiyaç yoktur ama eğerki garantici yani Defensive programming yapmak istiyorsak...
    /// ve birazdaha kendimizi sağlama almak istiyorsak bunu yapmakta fayda var.
    /// Veya türkçe veritabanı kullanıyosak bunu mutlaka yapmamız gerekiyor.
    /// </summary>
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable(@"Products", @"dbo");
            HasKey(p=>p.ProductId);

            Property(p=>p.ProductId).HasColumnName("ProductId");
            Property(p=>p.CategoryId).HasColumnName("CategoryId");
            Property(p=>p.ProductName).HasColumnName("ProductName");
            Property(p=>p.QuantityPerUnit).HasColumnName("QuantityPerUnit");
            Property(p => p.UnitPrice).HasColumnName("UnitPrice");
            Property(p=>p.UnitsInStock).HasColumnName("UnitsInStock");
        }
    }
}
