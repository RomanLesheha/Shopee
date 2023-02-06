﻿using FireSharp.Interfaces;
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
        static readonly IFirebaseConfig config = new FireSharp.Config.FirebaseConfig
        {
            AuthSecret = "hDxfemtaxxQxnalw3o3V4CInlZQ8foncdKlozKMm",
            BasePath = "https://shopee-a1-default-rtdb.europe-west1.firebasedatabase.app/"
        };
        IFirebaseClient client = new FireSharp.FirebaseClient(config);
        public IEnumerable<Product> ListOfProducts
        {
            get
            {
                List<Product> products = new List<Product>();
              
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
                List<Product> products = new List<Product>();
                FirebaseResponse response = client.Get("Products/");
                dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);

                if (data !=null)
                {
                    foreach (var item in data)
                    {
                        products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));
                    }
                }
                return products.Where(p=>p.CreationTime>=DateTime.Now.AddDays(-7));
            }
        }
        public bool CheckIdAlreadyCreated(int value)
        {
            List<Product> products = new List<Product>();
            FirebaseResponse response = client.Get("Products/");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            if (data != null)
            {
                foreach (var item in data)
                {
                    //products.Add(JsonConvert.DeserializeObject<Product>(((JProperty)item).Value.ToString()));

                    if (data["Id"]==value)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }

        public void AddNewProduct(Product product)
        {
            Random random = new Random();

            product.Availability = true;
            product.CreationTime = DateTime.Today;
            var value = random.Next(100000, 999999);
            product.Id = value;
            
            var data = product;
            FirebaseResponse response = client.Set("Products/" + data.Id, data);

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
