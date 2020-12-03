using EjderyaFramework.Business.Abstract;
using EjderyaFramework.Entities.Concrete;
using EjderyaFramework.MvcWebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EjderyaFramework.MvcWebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public ActionResult Index()
        {
            var model = new ProductListViewModel()
            {
               Products= _productService.GetAll()
            };
            return View(model);
        }
        public string Add()
        {
            _productService.Add(new Product { CategoryId = 1, ProductId = 1, ProductName = "GSM", QuantityPerUnit = "2", UnitPrice = 50000, UnitsInStock = 21 });


            return "Added";
        }

        public string AddUpdate()
        {
            _productService.TransactionalOperation(new Product
            {
                CategoryId = 1,
                ProductName = "Computer",
                QuantityPerUnit = "1",
                UnitPrice = 2

            }, new Product
            {
                CategoryId = 1,
                ProductName = "Computer 2",
                QuantityPerUnit = "1",
                UnitPrice = 10,
                ProductId = 2
            });
            return "Done";
        }
    }
}