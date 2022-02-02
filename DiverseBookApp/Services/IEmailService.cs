using DiverseBookApp.Models;
using System.Threading.Tasks;

namespace DiverseBookApp.Services
{
    public interface IEmailService
    {
        Task SendTestEmail(UserEmailOptions userEmailOptions);
    }
}