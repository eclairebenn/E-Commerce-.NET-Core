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
    public class CustomerController : Controller
    {
        private CommerceContext _context;
    
        public CustomerController(CommerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("customers")]
        public IActionResult Customers()
        {
            ViewBag.AllCustomers = _context.Customers.OrderByDescending(d => d.CreatedAt).ToList();
            return View();
        }

        [HttpPost]
        [Route("customer/add")]
        public IActionResult AddCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                List<Customer> AllCustomers = _context.Customers.ToList();
                if(AllCustomers.Exists(c => c.Name == customer.Name))
                {
                    TempData["customer"] = "That customer already exists";
                }
                else
                {
                    _context.Add(customer);
                    _context.SaveChanges();                    
                }
            }
            TempData["customer"] = "Invalid customer input";
            return RedirectToAction("Customers");
        }

        [HttpGet]
        [Route("customer/delete/{customerid}")]
        public IActionResult Delete(int customerid)
        {
            Customer CustDel = _context.Customers.SingleOrDefault(c => c.CustomerId == customerid);
            _context.Customers.Remove(CustDel);
            _context.SaveChanges();
            return RedirectToAction("Customers");
        }


    }
}