using Data;
using Generator;
using Microsoft.EntityFrameworkCore;
using Models;
using ViewModels;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        #region Dependecy

        protected MyApplicationDbContext Context { get; }
        public ILogger<UserRepository> Logger { get; }

        public UserRepository(MyApplicationDbContext context, ILogger<UserRepository> logger)
        {
            Context = context;
            Logger = logger;
        }

        #endregion \Dependecy

        #region Methods

        #region IsExistEmail

        public async Task<bool> IsExistEmailAsync(string email)
        {
            try
            {
                var _isExist = await Context.Users
                                        .AnyAsync(u => u.Email == email);

                return _isExist;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsExistEmail

        #region AddUserAsync

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                await Context.Users.AddAsync(user);
                await Context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \AddUserAsync

        #region IsCorrectPassword

        public async Task<bool> IsCorrectPasswordAsync(string newPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            try
            {
                var _isCorrect = HashGenerator.IsCorrectPassword(newPassword, passwordHash, passwordSalt);

                return _isCorrect;
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsCorrectPassword

        #region GetUser

        public async Task<User> GetUserAsync(string email)
        {
            try
            {
                var _user = await Context.Users
                                    .Where(u => u.Email == email)
                                    .FirstOrDefaultAsync();

                return _user;

            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task<User> GetUserAsync(int userId)
        {
            try
            {
                var _user = await Context.Users
                                        .FindAsync(userId);

                return _user;

            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetUser

        #region UpdateUser

        public async Task UpdateUserAsync(int userId, UpdateUserViewModel updateUserViewModel)
        {
            try
            {
                var _user = await Context.Users
                                       .FindAsync(userId);

                _user.Email = updateUserViewModel.Email;
                _user.FirstName = updateUserViewModel.Firstname;
                _user.LastName = updateUserViewModel.Lastname;

                await Task.Run(() =>
                {
                    Context.Users.Update(_user);
                });

                await Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        #endregion \UpdateUser

        #endregion \Methods
    }
}
