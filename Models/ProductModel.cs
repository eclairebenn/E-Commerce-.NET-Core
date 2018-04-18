using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Product : BaseEntity
    {
        public int ProductId { get; set; }
        [Required]
        public string Name {get;set;}

        [Required]
        [RegularExpression(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$")]
        public string ImageUrl {get;set;}
        [Required]
        public string Description {get;set;}
        [Required]
        public int Stock {get;set;}

        public decimal Price {get;set;}
        public List<Order> InOrders {get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Product()
        {
            InOrders = new List<Order>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}