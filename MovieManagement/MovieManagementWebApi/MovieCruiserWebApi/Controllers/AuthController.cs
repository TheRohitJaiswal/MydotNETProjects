using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieCruiserWebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace MovieCruiserWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] User login)
        {
            IActionResult response = Unauthorized();
           // var user = AuthenticateUser(login);

            if (true)
            {
                var tokenString = GenerateJSONWebToken(login);
                response = Ok(new { token = tokenString });
            }

            return response;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>() {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Name),
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            if(userInfo.UserId == 1)
            {
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"));
            }
            else if(userInfo.UserId>1)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Customer"));
            }
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        //private UserModel AuthenticateUser(User login)
        //{
        //    User user = null;

        //    //Validate the User Credentials
        //    //Demo Purpose, I have Passed HardCoded User Information
        //    if (user.Username == "Jignesh")
        //    {
        //        user = new UserModel { Username = "Jignesh Trivedi", EmailAddress = "test.btest@gmail.com", DateOfJoing = new DateTime(2010, 08, 02) };
        //    }
        //    return user;
        //}
    }
}
