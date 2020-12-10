using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EjderyaFramework.WebApi.Controllers
{
    public class ProductsController : ApiController
    {
        IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        public List<Product> Get()
        {
            return _productService.GetAll();
        }
    }
}
