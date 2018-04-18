using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private CommerceContext _context;
    
        public HomeController(CommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            List<Product> AllProducts = _context.Products.OrderByDescending(d => d.UpdatedAt).Take(5).ToList();
            List<Order> AllOrders = _context.Orders.OrderByDescending(o => o.CreatedAt).Include(u => u.Customer).Include(p => p.Product).Take(3).ToList();
            List<Customer> AllCustomers = _context.Customers.OrderByDescending(d => d.CreatedAt).Take(3).ToList();
            ViewBag.AllProducts = AllProducts;
            ViewBag.AllOrders = AllOrders;
            ViewBag.AllCustomers = AllCustomers;
            return View();
        }
        
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
