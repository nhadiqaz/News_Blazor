using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
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
        public AuthenticationsController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion \Dependency

        //=================================================================
        //=================================================================

        #region EndPoints

        [HttpPost]
        public async Task<IActionResult> Autentication(UserLogInViewModel userViewModel)
        {
            var _user = await ValidationUser(userViewModel);//temprory

            if (_user is null)
            {
                return Unauthorized();
            }

            var _claims = new List<Claim>
            {
                //new Claim(ClaimTypes.NameIdentifier,_user.UserId.ToString()),
                //new Claim(ClaimTypes.Name,_user.UserName.ToString()),

                new Claim("UserId",_user.UserId.ToString()),
                new Claim("UserName",_user.UserName.ToString()),
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

        #region Method

        private async Task<User> ValidationUser(UserLogInViewModel userViewModel)
        {
            var _user = new User
            {
                UserId = 1,
                UserName = "Nhadiqaz",
                Password = "123",
                FirstName = "Hadi",
                LastName = "Najafapour",
                Email = "hadinajafpourf5@gmail.com",
                CreateDate = DateTime.Now,
            };

            return _user;
        }

        #endregion \Method

    }
}
