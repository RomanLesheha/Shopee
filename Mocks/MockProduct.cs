using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shopee.Interfeces;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Mocks
{
    public class MockProduct : IProducts
    {
        readonly IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "hDxfemtaxxQxnalw3o3V4CInlZQ8foncdKlozKMm",
            BasePath = "https://shopee-a1-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client;
        public IEnumerable<Product> ListOfProducts
        {
            get
            {
                List<Product> products = new List<Product>();
                client = new FireSharp.FirebaseClient(config);
              
                FirebaseResponse response = client.Get("Products/");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));
                    }
                }
                return products;
            }
        }

        public IEnumerable<Product> ListOfNewProducts
        {
            get
            {
                return new List<Product>();
            }
        }

        public void AddNewProduct(Product product)
        {
            client = new FireSharp.FirebaseClient(config);
            Random random = new Random();

            product.Availability = true;
            product.CreationTime = DateTime.Today;
            product.Id = random.Next(1000000, 9999999);


            var data = product;
            FirebaseResponse response = client.Set("Products/" + data.Id, data);

        }
    }
}
