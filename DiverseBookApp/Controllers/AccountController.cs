using DiverseBookApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiverseBookApp.Controllers
{
    public class AccountController : Controller
    {
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [Route("signup")]
        public IActionResult Signup(SignupUserModel signupUserModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();

            }
            return View();
        }
    }
}
