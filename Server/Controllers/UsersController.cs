using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Models;
using Repositories;
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
                    var _message = "Entered Email is exist";
                    var _error = new ErrorMessageViewModel
                    {
                        Message = _message,
                    };

                    return BadRequest(_error);
                }

                var _user = registerUserViewModel.ConvertTo_User();

                _user = await UserRepository.AddUserAsync(_user);

                return Ok(_user);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                await Console.Out.WriteLineAsync(ex.Message);
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
                await Console.Out.WriteLineAsync(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \IsExistEmail


        #endregion \EndPoints
    }
}
