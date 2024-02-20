using FinalProjectUni.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using weka;

namespace FinalProjectUni.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var user = User.Identity.GetUserName();
            if (user == "admin@admin.com")
            { return View("~/Views/SystemMangerPages/index.cshtml"); }

            else if (user == "adminf@admin.com")
            { return View("~/Views/SystemMangerPages/Edit.cshtml"); }

            else if (user == "adminm@admin.com")
            { return View("~/Views/SystemMangerPages/delete.cshtml"); }

            else return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Loancal()
        {
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
