using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Dynamic;
using practise.BookStoreApp.Models;

namespace practise.BookStoreApp.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public int MyProperty { get; set; }
        public ViewResult Index()
        {
            ViewData["property1"] = "Princy Lora";
            ViewData["book"] = new BookModel { Id = 21, Author = "Malaika" };
            return View();
        }
        public ViewResult AboutUs()
        {
            ViewBag.Title = "Princyy";
            dynamic data = new ExpandoObject();
            data.Id = 1;
            data.Name = "PrincLora";
            ViewBag.Data = data;
            return View();
        }

        [Route("Contact")]
        public ViewResult ContactUs()
        {
            MyProperty = 1997;
            return View();
        }
    }
}
