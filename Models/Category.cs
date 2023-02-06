using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Models
{
    public class Category
    {
        public string Name { get; set; }
        public string CategoryImage { get; set; }

        public List<Product> AllProducts { get; set; } 

       
    }
}
