using ViewModels;

namespace Services
{
    public interface IUserService
    {
        Task<bool> IsExistEmailAsync(string email);
        Task AddUserAsync(RegisterUserViewModel registerUserViewModel);
    }
}
