using DiverseBookApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly SignInManager<ApplicationUsers> _signInManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager, SignInManager<ApplicationUsers> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IdentityResult> CreateUser(SignupUserModel signupUserModel)
        {

            var user = new ApplicationUsers()
            {
                FirstName = signupUserModel.FirstName,
                LastName = signupUserModel.LastName,
                Email = signupUserModel.Email,
                UserName = signupUserModel.Email,
            };
            var result = await _userManager.CreateAsync(user, signupUserModel.Password);
            return result;
        }

        public async Task<SignInResult> UserLogin(LoginModel loginModel)
        {
            return await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, loginModel.RememberMe, false);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
