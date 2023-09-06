using ViewModels;

namespace Services
{
    public interface IAutenticationService
    {
        Task<string> GetTokenAsync(UserLogInViewModel userViewModel);
    }
}
