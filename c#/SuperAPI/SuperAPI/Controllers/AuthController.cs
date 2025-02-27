using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using Super.Core.Models;
using Super.Core.Service;
using Super.Service;
using SuperAPI.Models;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration, IUserService userService)

        {
            _configuration = configuration;
            _userService = userService;
        }

        // POST api/<AuthController>
        //[AllowAnonymous]
        [HttpPost]
        public IActionResult Login([FromBody] UserLoginModel userLoginModel)
        {

            var findUser = _userService.GetUserByName(userLoginModel.UserName);
            if (findUser == null)
            {
                return BadRequest(new { message = "User not found" });
            }
            else if (!BCrypt.Net.BCrypt.Verify(userLoginModel.Password, findUser.Password))
{
                return Unauthorized(new { message = "Incorrect password" });
            }

            var claims = new List<Claim>()
{
                new Claim(ClaimTypes.Name,findUser.UserName ),
                new Claim(ClaimTypes.Role, string.Join(",", findUser.UserRoles.Select(r => r.Role.Name.ToString())))
                };
            var secretKey = new
            SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
            var signinCredentials = new SigningCredentials(secretKey,
            SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(720),
            signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return Ok(new { Token = tokenString });
        }
    }
}

    

       

       
    
