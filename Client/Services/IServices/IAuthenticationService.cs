using ViewModels;

namespace Services
{
    public interface IAuthenticationService
    {
        Task<string> GetTokenAsync(LogInUserViewModel logInUserViewModel);
        Task<string> GetTokenUpdateEmailAsync(string email);
    }
}
