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

namespace Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        #region Dependency
        private IConfiguration Configuration { get; }
        public IUserRepository UserRepository { get; }

        public AuthenticationsController(IConfiguration configuration, IUserRepository userRepository)
        {
            Configuration = configuration;
            UserRepository = userRepository;
        }

        #endregion \Dependency

        //=================================================================
        //=================================================================

        #region EndPoints

        [HttpPost]
        public async Task<IActionResult> Autentication(LogInUserViewModel logInUserViewModel)
        {
            if (await UserRepository.IsExistEmailAsync(logInUserViewModel.Email) == false)
            {
                var _error = new ErrorMessageViewModel
                {
                    Message = "کاربری با این پست الکتریکی در سیستم موجود نیست"
                };

                return NotFound(_error);
            }

            var _user = await UserRepository.GetUserAsync(logInUserViewModel.Email);

            if (await UserRepository.IsCorrectPasswordAsync(logInUserViewModel.Password, _user.PasswordHash, _user.PasswordSalt) == false)
            {
                var _error = new ErrorMessageViewModel
                {
                    Message = "رمز عبور به درستی وارد نشده است"
                };

                return BadRequest(_error);
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

        #endregion EndPoints
    }
}
