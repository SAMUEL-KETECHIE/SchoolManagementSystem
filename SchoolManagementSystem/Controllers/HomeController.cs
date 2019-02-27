using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Models;

namespace SchoolManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["user"] = "Admin";
            if (ViewData["user"].ToString() != "Admin")
                return View();
            return View(Admin());
        }

        public IActionResult Login()
        {
            ViewData["user"] = "Admin";
            return View();
        }

        public IActionResult Subjects()
        {
            ViewData["user"] = "Admin";
            return View();
        }

        public IActionResult Students()
        {
            ViewData["user"] = "Admin";

            return View();
        }

        public IActionResult Teachers()
        {
            ViewData["user"] = "Admin";

            return View();
        }

        public IActionResult Admin()
        {
            ViewData["user"] = "Admin";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
