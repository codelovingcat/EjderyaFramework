using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Business.DependencyResolvers.Ninject;
using EjderyaFramework.Business.ServiceContracts.Wcf;
using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProductDetailService
/// </summary>
public class ProductDetailService : IProductDetailService
{
    private IProductService _productService = InstanceFactory.GetInstance<IProductService>();
    public List<Product> GetAll()
    {
        return _productService.GetAll();
    }
}