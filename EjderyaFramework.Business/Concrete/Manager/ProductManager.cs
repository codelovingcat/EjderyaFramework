using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Business.ValidationRules.FluentValidation;
using EjderyaFramework.Core.Validation.FluentValidation;
using EjderyaFramework.DataAccess.Abstract;
using EjderyaFramework.Entities.Concrete;
using EjderyaFramework.Core.Aspects.Pastsharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EjderyaFramework.Core.Aspects.TransactionAspects;
using EjderyaFramework.Core.Aspects.Pastsharp.CacheAspects;
using EjderyaFramework.Core.CrossCuttingConcerns.Caching.Microsoft;
using EjderyaFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using EjderyaFramework.Core.Aspects.Pastsharp.LogAspects;
using EjderyaFramework.Core.Aspects.Pastsharp.PerformanceAspects;
using System.Threading;
using EjderyaFramework.Core.Aspects.Pastsharp.AuthorizationAspects;
using AutoMapper;
using EjderyaFramework.Core.Utilities.Mappings;

namespace EjderyaFramework.Business.Concrete.Manager
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        private readonly IMapper _mapper;
        public ProductManager(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        [PerformanceCounterAspect(2)]
        [SecuredOperation(Roles = "Admin,Editor,Student")]
        public List<Product> GetAll()
        {
            //Thread.Sleep(3000);
            //return _productDal.GetList().Select(p => new Product
            //{
            //    CategoryId = p.CategoryId,
            //    ProductId = p.ProductId,
            //    ProductName = p.ProductName,
            //    QuantityPerUnit = p.QuantityPerUnit,
            //    UnitPrice = p.UnitPrice,
            //    UnitsInStock = p.UnitsInStock

            //}).ToList();

            //var product = MatToSameTypeLİst<Product>(_productDal.GetList());

            //var product = AutoMapperHelper.MapToSameTypeList(_productDal.GetList());
            var product =_mapper.Map<List<Product>>(_productDal.GetList());
            return product;
        }


        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            return _productDal.Update(product);
        }

        [TransactionScopeAspect]
        [FluentValidationAspect(typeof(ProductValidatior))]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            // Business Codes
            _productDal.Update(product2);
        }
    }
}
