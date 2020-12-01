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

namespace EjderyaFramework.Business.Concrete.Manager
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;
        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        [FluentValidationAspect(typeof(ProductValidatior))]
        [CacheRemoveAspect(typeof(MemoryCacheManager))]
        public Product Add(Product product)
        {
           // ValidatorTool.FluentValidate(new ValidatiorTool(), product);
            return _productDal.Add(product);
        }

        [CacheAspect(typeof(MemoryCacheManager))]
        public List<Product> GetAll()
        {
            return _productDal.GetList();
        }

        public Product GetById(int id)
        {
            return _productDal.Get(p => p.ProductId == id);
        }

        [FluentValidationAspect(typeof(ProductValidatior))]
        public Product Update(Product product)
        {
            //ValidatorTool.FluentValidate(new ValidatiorTool(), product);
            return _productDal.Update(product);
        }

        [TransactionScopeAspect]
        public void TransactionalOperation(Product product1, Product product2)
        {
            _productDal.Add(product1);
            // Business Codes
            _productDal.Update(product2);
        }
    }
}
