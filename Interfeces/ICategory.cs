using Shopee.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Interfeces
{
    public interface ICategory
    {
        public IEnumerable<Category> GetAllCategory { get; }

        public IEnumerable<Product> GetAllProductsInCategory(string CategoryName);

        public void AddNewCategory(Category category);

    }
}
