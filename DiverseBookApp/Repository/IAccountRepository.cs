using DiverseBookApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUser(SignupUserModel signupUserModel);
        Task<SignInResult> UserLogin(LoginModel loginModel);
    }
}