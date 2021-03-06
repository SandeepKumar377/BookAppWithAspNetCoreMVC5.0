using DiverseBookApp.Models;
using DiverseBookApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUsers> _signInManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager, 
            IUserService userService,
            IEmailService emailService,
            IConfiguration configuration,
            SignInManager<ApplicationUsers> signInManager)
        {
            _userManager = userManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _signInManager = signInManager;
        }

        public async Task<ApplicationUsers> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
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
            if (result.Succeeded)
            {
                await GenerateEmailConfirmationTokenAsync(user);
            }
            return result;
        }

        public async Task GenerateEmailConfirmationTokenAsync(ApplicationUsers user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendConfirmationEmail(user, token);
            }
        }
        
        public async Task GenerateForgotPasswordTokenAsync(ApplicationUsers user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
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
        public async Task<IdentityResult> ConfirmEmail(string uid, string token)
        {
            return await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
        }

        public async Task<IdentityResult> ResetPassword(ResetPasswordModel model)
        {
            return await _userManager.ResetPasswordAsync(await _userManager.FindByIdAsync(model.UserId), model.Token, model.NewPassword);
        }

        private async Task SendConfirmationEmail(ApplicationUsers user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token)),
                }
            };
            await _emailService.SendEmailForEmailConfirmation(options);
        }
        
        private async Task SendForgotPasswordEmail(ApplicationUsers user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;

            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() { user.Email },
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new KeyValuePair<string, string>("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token)),
                }
            };
            await _emailService.SendEmailForForgotPassword(options);
        }
    }
}
