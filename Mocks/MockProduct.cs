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

namespace Shopee.Mocks
{
    public class MockProduct : IProducts
    {
        static readonly IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "hDxfemtaxxQxnalw3o3V4CInlZQ8foncdKlozKMm",
            BasePath = "https://shopee-a1-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client = new FireSharp.FirebaseClient(config);

        
        public IEnumerable<Product> ListOfProducts(int parameter)
        {
                List<Product> products = new List<Product>();
                try
                {
                    FirebaseResponse response = client.Get("Products/");
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            switch (parameter)
            {
                case 0: 
                    return products;
                case 1:
                    List<Product> objListOrder = products.OrderBy(order => order.Price).ToList();
                    return objListOrder;
                case 2:
                    List<Product> objListOrderRevers = products.OrderBy(order => order.Price).Reverse().ToList();
                    return objListOrderRevers;
                case 3:
                    List<Product> objListOrderTime = products.OrderBy(order => order.CreationTime).Reverse().ToList();
                    return objListOrderTime;
                default:
                    break;
            }
            return products;
        }

        public IEnumerable<Product> ListOfNewProducts
        {
            get
            {
                List<Product> products = new List<Product>();
                try
                {
                    FirebaseResponse response = client.Get("Products/");
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

                    if (data != null)
                    {
                        foreach (var item in data)
                        {
                            products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
                
                return products.Where(p=>p.CreationTime>=DateTime.Now.AddDays(-7));
            }
        }

        public void AddNewProduct(Product product)
        {
            Random random = new Random();

            product.Availability = true;
            product.CreationTime = DateTime.Today;
            product.Id = random.Next(1000000, 9999999);

            var data = product;
            SetResponse response = client.Set("Products/" + data.Id, data);

        }

        public Product GetSelectedProduct(int ProductID)
        {
            
            List<Product> products = new List<Product>();

            FirebaseResponse response = client.Get("Products/");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

            foreach (var item in data)
            {
                products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));
            }

            return products.Find(p => p.Id == ProductID);
        }
    }
}
