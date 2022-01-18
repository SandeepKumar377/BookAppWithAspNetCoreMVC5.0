using DiverseBookApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [ViewData]
        public string CustomProperty { get; set; }
        [ViewData]
        public string Title { get; set; }
        [ViewData]
        public string Book { get; set; }
        public ViewResult Index()
        {
            var userId = _userService.GetUserId();
            var isLoggedIn = _userService.IsAuthenticated();

            Title = "Home from Ctrl";
            CustomProperty = "Sandeep kumar";
            return View();
        }
        public ViewResult AboutUs()
        {
            Title = "About us from Ctrl";
            return View();
        }
        public ViewResult ContactUs()
        {
            Title = "Contact us from Ctrl";
            return View();
        }
    }
}
