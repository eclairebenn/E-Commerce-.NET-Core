using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models
{
    public class CommerceContext : DbContext
    {
        public CommerceContext(DbContextOptions<CommerceContext> options) : base(options) { }

        public DbSet<Customer> Customers {get;set;}
        public DbSet<Product> Products {get;set;}
        public DbSet<Order> Orders {get;set;}

    }
}