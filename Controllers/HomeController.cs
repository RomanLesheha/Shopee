using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Shopee.Interfeces;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProducts _product;
        private readonly ICategory _category;

        public HomeController(ILogger<HomeController> logger, IProducts products, ICategory categories)
        {
            _logger = logger;
            _product = products;
            _category = categories;
        }

        public IActionResult Index()
        {
            var listofnew = _product.ListOfNewProducts;
            (IEnumerable<Product>, IEnumerable<Category>) obj;
            obj.Item1 = _product.ListOfNewProducts;
            obj.Item2 = _category.GetAllCategory;
            return View(obj);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
