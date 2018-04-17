using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace ecommerce.Controllers
{
    public class ProductController : Controller
    {
        private CommerceContext _context;
    
        public ProductController(CommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("products")]
        public IActionResult Products()
        {
            ViewBag.AllProducts = _context.Products.OrderByDescending(d => d.UpdatedAt).ToList();
            return View();
        }

        [HttpPost]
        [Route("product/add")]
        public IActionResult AddProduct(Product product)
        {
            if(ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Products");
            }
            return RedirectToAction("Products");
        }

    }
}