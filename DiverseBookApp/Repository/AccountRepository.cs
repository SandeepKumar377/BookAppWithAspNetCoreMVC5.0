using DiverseBookApp.Models;
using DiverseBookApp.Services;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IUserService _userService;
        private readonly SignInManager<ApplicationUsers> _signInManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager, 
            IUserService userService,
            SignInManager<ApplicationUsers> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
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

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, changePasswordModel.CurrentPassword, changePasswordModel.NewPassword);
        }
    }
}
