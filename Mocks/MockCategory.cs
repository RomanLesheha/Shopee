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

        public IEnumerable<Category> GetAllCategory
        {
            get
            {
                List<Category> listOfAllCategories = new List<Category>();
                try
                {
                    FirebaseResponse response = client.Get($"Categories/");
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            listOfAllCategories.Add(JsonConvert.DeserializeObject<Category>(((JProperty)item).Value.ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
                return listOfAllCategories;
            }
        }

        public void AddNewCategory(Category category)
        {
            var data = category;
            SetResponse response = client.Set("Categories/" + data.Name, data);
        }

       
        public IEnumerable<Product> GetAllProductsInCategory(string CategoryName)
        {
            List<Product> productsInCategory = new List<Product>();
            try
            {
                FirebaseResponse response = client.Get($"Categories/{CategoryName}/AllProducts/");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        productsInCategory.Add(JsonConvert.DeserializeObject<Product>(((JObject)item).ToString()));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            
            return productsInCategory;
        }
    }
}
