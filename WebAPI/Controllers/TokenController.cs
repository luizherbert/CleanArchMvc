using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchMvc.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IAuthenticate _authetincate;
        private readonly IConfiguration _configuration;

        public TokenController(IAuthenticate authetincate, IConfiguration configuration)
        {
            _authetincate = authetincate;
            _configuration = configuration;
        }

        [HttpPost("CreateUser")]
        [AllowAnonymous]
        public async Task<ActionResult> CreateUser([FromBody] LoginModel user)
        {
            var result = await _authetincate.RegisterUser(user.Email, user.Password);

            if (result)
            {
                return Ok($"User {user.Email} was created");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create user");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        [AllowAnonymous]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("LoginModel is null");
            }

            var result = await _authetincate.Authenticate(loginModel.Email, loginModel.Password);

            if (result)
            {
                return GenerateToken(loginModel);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }

        }

        private UserToken GenerateToken(LoginModel loginModel)
        {
            var claims = new[] {
                new Claim("email", loginModel.Email),
                new Claim("password", loginModel.Password),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var privateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var signInCredentials = new SigningCredentials(privateKey, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(10);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: signInCredentials
            );

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
}
