using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FullStack_API.Models;

namespace WebApplication10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _Configuration;

        public AuthController(IConfiguration Configuration)
        {
            _Configuration = Configuration;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Auth([FromBody] EmployeeJWT empJWT)
        {
            IActionResult response = Unauthorized();

            if (empJWT != null && empJWT.UserName.Equals("hamza@gmail.com") && empJWT.Password.Equals("hello"))
            {
                var issuer = _Configuration["Jwt:Issuer"];
                var audience = _Configuration["Jwt:Audience"];
                var key = Encoding.UTF8.GetBytes(_Configuration["Jwt:SecretKey"]);

                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, empJWT.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, empJWT.UserName)
                };

                var expires = DateTime.UtcNow.AddMinutes(10);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = expires,
                    Issuer = issuer,
                    Audience = audience,
                    SigningCredentials = signingCredentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return Ok(jwtToken);
            }

            return response;
        }
    }
}
