using DiverseBookApp.Models;
using DiverseBookApp.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiverseBookApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignupUserModel signupUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUser(signupUserModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(signupUserModel);
                }
                ModelState.Clear();
                return View();
            }
            return View();
        }
    }
}
