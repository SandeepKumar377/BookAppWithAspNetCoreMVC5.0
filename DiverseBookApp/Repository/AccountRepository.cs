using DiverseBookApp.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace DiverseBookApp.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUsers> _userManager;

        public AccountRepository(UserManager<ApplicationUsers> userManager)
        {
            _userManager = userManager;
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
    }
}
