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
        public readonly ICategory _categories;

        public ProductController(IProducts IProducts , ICategory ICategory)
        {
            _products = IProducts;
            _categories = ICategory;
        }
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method to display list of all products on page 
        /// </summary>
        /// <returns></returns>
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
                Category category = new Category();
                category.AllProducts = (List<Product>)_categories.GetAllProductsInCategory(product.Category.Name);
                category.AllProducts.Add(product);
                category.Name = product.Category.Name;
                category.CategoryImage = " ";
                
                

                _products.AddNewProduct(product);
                _categories.AddNewCategory(category);
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
