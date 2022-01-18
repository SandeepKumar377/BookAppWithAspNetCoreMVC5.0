namespace DiverseBookApp.Services
{
    public interface IUserService
    {
        string GetUserId();
        bool IsAuthenticated();
    }
}