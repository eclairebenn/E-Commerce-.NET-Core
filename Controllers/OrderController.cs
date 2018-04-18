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
using Stripe;

namespace ecommerce.Controllers
{
    public class OrderController : Controller
    {
        private CommerceContext _context;
    
        public OrderController(CommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("orders")]
        public IActionResult Orders()
        {
            List<Order> AllOrders = _context.Orders.OrderByDescending(d => d.CreatedAt).Include(c => c.Customer).Include(p => p.Product).ToList();
            List<Product> AllProducts = _context.Products.OrderByDescending(d => d.CreatedAt).ToList();
            List<Customer> AllCustomers = _context.Customers.OrderByDescending(d => d.CreatedAt).ToList();
            ViewBag.AllOrders = AllOrders;            
            ViewBag.AllProducts = AllProducts;
            ViewBag.AllCustomers = AllCustomers;
            return View();
        }

        [HttpPost]
        [Route("order/add")]
        public IActionResult AddOrder(Order order)
        {
            if(ModelState.IsValid)
            {
                Product product = _context.Products.SingleOrDefault(p => p.ProductId == order.ProductId);
                if(product.Stock - order.Quantity < 0)
                {
                    TempData["stock"] = "There is not enough stock to make this order";
                }
                else
                {
                    _context.Add(order);
                    product.Stock = product.Stock - order.Quantity;
                    product.UpdatedAt = DateTime.Now;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Orders");
        }

        [HttpPost]
        [Route("Order/Charge/{productid}")]
        public IActionResult Charge(string  stripeEmail, string stripeToken, int productid)
        {
            var customers = new StripeCustomerService();
            var charges = new StripeChargeService();

            var customer = customers.Create(new StripeCustomerCreateOptions {
            Email = stripeEmail,
            SourceToken = stripeToken
            });
            Product product = _context.Products.SingleOrDefault(p => p.ProductId == productid);
            var charge = charges.Create(new StripeChargeCreateOptions {
            Amount = (int)product.Price*100,
            Description = "Sample Charge",
            Currency = "usd",
            CustomerId = customer.Id
            });

            List<Order> AllOrders = _context.Orders.OrderByDescending(d => d.CreatedAt).Include(c => c.Customer).Include(p => p.Product).ToList();
            List<Product> AllProducts = _context.Products.OrderByDescending(d => d.CreatedAt).ToList();
            List<Customer> AllCustomers = _context.Customers.OrderByDescending(d => d.CreatedAt).ToList();
            ViewBag.AllOrders = AllOrders;            
            ViewBag.AllProducts = AllProducts;
            ViewBag.AllCustomers = AllCustomers;

            return View("Orders");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}