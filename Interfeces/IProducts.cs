using Microsoft.AspNetCore.Http;
using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Interfeces
{
    public interface IProducts
    {
        public IEnumerable<Product> ListOfProducts { get; }

        public IEnumerable<Product> ListOfNewProducts { get; }

        public void AddNewProduct(Product product);

        public Product GetSelectedProduct(int id);
    }
}
