using Firebase.Auth;
using Firebase.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Shopee.Interfeces;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Shopee.Controllers
{
    public class ProductController :Controller
    {
        private readonly string ApiKey = "AIzaSyA7fTzLVnw_1KVatG3rNFJdRqlhdr61pc4";
        private readonly string Bucket = "shopee-a1.appspot.com";
        private readonly string AuthEmail = "shopee@gmail.com";
        private readonly string AuthPassword = "roman123";

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
        public IActionResult ListOfProducts(int parameters)
        {
            var list = _products.ListOfProducts(parameters);
            return View(list);
        }
        public IActionResult AddNewProduct()
        {
            return View();
        }
        /// <summary>
        /// Method to add new Product to FireBase Realtime DataBase and upload Product Image to Firebase Storage
        /// </summary>
        /// <param name="product"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewProduct(Product product , IFormFile file)
        {
            if (ModelState.IsValid)
            {
                var stream = file.OpenReadStream(); //cast IFormFile to Stream by copying data

                var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
                var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

                // you can use CancellationTokenSource to cancel the upload midway
                var cancellation = new CancellationTokenSource();

                var task = new FirebaseStorage(
                    Bucket,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
                    })
                    .Child("Image")
                    .Child("ProductsImage")
                    .Child(file.FileName)
                    .PutAsync(stream, cancellation.Token);

                try
                {
                    Category category = new Category();
                    category.AllProducts = (List<Product>)_categories.GetAllProductsInCategory(product.Category.Name);//call method with return list of Products which have already been in DB
                    category.AllProducts.Add(product); // Adding new item to category list of products
                    category.Name = product.Category.Name;
                    product.DiscountedPrice = (uint?)(product.Price - (product.Price * (product.Discount / 100)));
                    product.Image = await task; 

                    _products.AddNewProduct(product); //adding new product to DB
                    _categories.AddNewCategory(category); //changing information of category by adding new item to Category List of <Products>

                    return RedirectToAction("ListOfProducts");
                }
                catch (Exception ex)
                {
                    ViewBag.error = $"Exception was thrown: {ex}";
                }
            }
            return BadRequest();
        }
        /// <summary>
        /// Method to return page with all information about selected product
        /// </summary>
        /// <param name="ProductID"></param>
        /// <returns></returns>
        public IActionResult SelectedProduct(int ProductID)
        {
            var obj = _products.GetSelectedProduct(ProductID);
            return View(obj);
        }
    }
}
