using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shopee.Interfeces;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Mocks
{
    public class MockCategory : ICategory
    {
        static readonly IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "hDxfemtaxxQxnalw3o3V4CInlZQ8foncdKlozKMm",
            BasePath = "https://shopee-a1-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client = new FireSharp.FirebaseClient(config);

        public IEnumerable<Category> GetAllCategory => throw new NotImplementedException();


        public void AddNewCategory(Category category)
        {
            var data = category;
            SetResponse response = client.Set("Categories/" + data.Name, data);
        }

       

        public IEnumerable<Product> GetAllProductsInCategory(string CategoryName)
        {
            List<Product> productsInCategory = new List<Product>();
         
                FirebaseResponse response = client.Get($"Categories/{CategoryName}/AllProducts/");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        productsInCategory.Add(JsonConvert.DeserializeObject<Product>(((JObject)item).ToString()));
                    }
                }

            
            return productsInCategory;
        }
    }
}
