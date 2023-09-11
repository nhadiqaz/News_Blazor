using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ViewModels;
using ViewModels.User;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        #region Dependency
        private IConfiguration Configuration { get; }
        public IUserRepository UserRepository { get; }
        public ILogger<AuthenticationsController> Logger { get; }

        public AuthenticationsController(IConfiguration configuration, IUserRepository userRepository, ILogger<AuthenticationsController> logger)
        {
            Configuration = configuration;
            UserRepository = userRepository;
            Logger = logger;
        }

        #endregion \Dependency

        //=================================================================
        //=================================================================

        #region EndPoints

        #region Autentication

        [HttpPost]
        public async Task<IActionResult> Authentication(LogInUserViewModel logInUserViewModel)
        {
            try
            {
                if (await UserRepository.IsExistEmailAsync(logInUserViewModel.Email) == false)
                {
                    var _message = "کاربری با این پست الکتریکی در سیستم موجود نیست";

                    return NotFound(_message);
                }

                var _user = await UserRepository.GetUserAsync(logInUserViewModel.Email);

                if (await UserRepository.IsCorrectPasswordAsync(logInUserViewModel.Password, _user.PasswordHash, _user.PasswordSalt) == false)
                {
                    var _message = "رمز عبور به درستی وارد نشده است";

                    return BadRequest(_message);
                }

                var _claims = new List<Claim>
                {
                //new Claim(ClaimTypes.NameIdentifier,_user.UserId.ToString()),
                //new Claim(ClaimTypes.Name,_user.UserName.ToString()),

                new Claim("UserId",_user.UserId.ToString()),
                new Claim("Email",_user.Email.ToString()),
                };

                var _securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Authentication:SecretForKey"]));

                var _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

                var _jwtSecurityToken = new JwtSecurityToken(
                    issuer: Configuration["Authentication:Issuer"],
                    audience: Configuration["Authentication:Audience"],
                    claims: _claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddDays(3),
                    signingCredentials: _signingCredentials
                    );

                var _token = new JwtSecurityTokenHandler().WriteToken(_jwtSecurityToken);

                return Ok(_token);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \Autentication

        #region AuthenticationUpdate

        [HttpPost]
        public async Task<IActionResult> AuthenticationUpdateEmail(UpdateTokenViewModel updateTokenViewModel)
        {
            try
            {
                var _email = updateTokenViewModel.Email;

                string? _token = null;

                var _user = await UserRepository.GetUserAsync(_email);

                await Task.Run(() =>
                {

                    var _claims = new List<Claim>()
                    {
                    new Claim("UserId",_user.UserId.ToString()),
                    new Claim("Email",_email),
                    };

                    var _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretForKey"]));

                    var _signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

                    var _jwtSecurityToken = new JwtSecurityToken(
                        issuer: Configuration["Authentication:Issuer"],
                        audience: Configuration["Authentication:Audience"],
                        claims: _claims,
                        notBefore: DateTime.Now,
                        expires: DateTime.Now.AddDays(3),
                        signingCredentials: _signingCredentials
                        );

                    _token = new JwtSecurityTokenHandler().WriteToken(_jwtSecurityToken);
                });

                return Ok(_token);
            }
            catch (Exception ex)
            {
                Logger.LogCritical(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #endregion \AuthenticationUpdate

        #endregion EndPoints
    }
}
