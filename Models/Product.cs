using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shopee.Models
{
    public class Product
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int Id { get; set; }
        [Required]
        public uint Price { get; set; }

        public bool Availability { get; set; }

        public double? Discount { get; set; }

        public uint? DiscountedPrice { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public virtual Category Category { get; set; }

        public uint? SavingMoney()
        {
            return Price - DiscountedPrice;
        }
    }
}
