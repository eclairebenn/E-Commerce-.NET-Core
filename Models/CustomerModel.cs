using System;
using System.Collections.Generic;

namespace ecommerce.Models
{
    public class Customer : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Order> Orders {get;set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
            CreatedAt = DateTime.Now;
            UpdatedAt = DateTime.Now;
        }
    }
}