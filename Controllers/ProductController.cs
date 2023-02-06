using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.AspNetCore.Mvc;
using Shopee.Interfeces;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Controllers
{
    public class ProductController : Controller
    {
        public readonly IProducts _products;

        public ProductController(IProducts IProducts)
        {
            _products = IProducts;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListOfProducts()
        {
            var list = _products.ListOfProducts;
            return View(list);
        }
        public IActionResult AddNewProduct()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _products.AddNewProduct(product);
            }
            return View("AddNewProduct");
        }
        public IActionResult SelectedProduct(int ProductID)
        {
            var obj = _products.GetSelectedProduct(ProductID);
            return View(obj);
        }
    }
}
