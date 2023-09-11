using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Identity.Client;
using Models;
using Repositories;
using Resources;
using ViewModels;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region Dependency

        protected IUserRepository UserRepository { get; }
        protected ILogger<UsersController> Logger { get; }

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            UserRepository = userRepository;
            Logger = logger;
        }

        #endregion \Dependency

        #region EndPoints

        #region RegisterUser

        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(RegisterUserViewModel registerUserViewModel)
        {
            try
            {
                if (TryValidateModel(registerUserViewModel) == false)
                {
                    return BadRequest();
                }

                if (await UserRepository.IsExistEmailAsync(registerUserViewModel.Email) == true)
                {
                    //var _message = "پست الکتریکی وارد شده در سیستم موجود است";
                    var _message = ErrorMessage.IsExistEmail;

                    return BadRequest(_message);
                }

                var _user = registerUserViewModel.ConvertTo_User();

                _user = await UserRepository.AddUserAsync(_user);

                return Ok(_user);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \RegisterUser

        #region IsExistEmail

        [HttpGet("{email}")]
        public async Task<ActionResult<bool>> IsExistEmail(string email)
        {
            try
            {
                var _isExist = await UserRepository.IsExistEmailAsync(email);

                return Ok(_isExist);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsExistEmail

        #region GetUser

        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUser(int userId)
        {
            try
            {
                var _user = await UserRepository.GetUserAsync(userId);

                if (_user is null)
                {
                    var _message = "کاربر مورد نظر یافت نشد";

                    return NotFound(_message);
                }

                return Ok(_user);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \GetUser

        #region UpdateUser

        [HttpPatch("{userId}")]

        public async Task<ActionResult<User>> UpdateUser(int userId, UpdateUserViewModel updateUserViewModel)
        {
            try
            {
                if (TryValidateModel(updateUserViewModel) == false)
                {
                    return BadRequest();
                }

                await UserRepository.UpdateUserAsync(userId, updateUserViewModel);

                var _updateUser = await UserRepository.GetUserAsync(userId);

                return Ok(_updateUser);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \UpdateUser

        #endregion \EndPoints
    }
}
