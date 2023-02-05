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

        public void AddNewProduct(Product product);

        public IEnumerable<Product> ListOfNewProducts { get; }
    }
}
