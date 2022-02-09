using DiverseBookApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignupUserModel signupUserModel);
        Task<IdentityResult> ResetPassword(ResetPasswordModel model);
        Task<SignInResult> UserLogin(LoginModel loginModel);
        Task Logout();
        Task<IdentityResult> ChangePassword(ChangePasswordModel changePasswordModel);
        Task<IdentityResult> ConfirmEmail(string uid, string token);
        Task GenerateEmailConfirmationTokenAsync(ApplicationUsers user);
        Task<ApplicationUsers> GetUserByEmailAsync(string email);
        Task GenerateForgotPasswordTokenAsync(ApplicationUsers user);
    }
}