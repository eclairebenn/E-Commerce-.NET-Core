using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ecommerce.Models
{
    public class Order : BaseEntity
    {
        public int OrderId { get; set; }

        [Required]
        [Range(1,50)]
        public int Quantity {get;set;}
        [Required]
        [Display(Name="Customer")]
        public int CustomerId {get;set;}
        public Customer Customer {get;set;}
        [Required]
        [Display(Name="Product")]
        public int ProductId {get;set;}
        public Product Product {get;set;}
        public DateTime CreatedAt { get; set; }
        public Order()
        {
            CreatedAt = DateTime.Now;
        }
    }
}