using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Super.Core.Service;
using SuperAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        var roles = findUser.UserRoles?.Select(ur => ur.Role.Name).ToList() ?? new List<string>();
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, findUser.UserName),
            new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", string.Join(",", roles))
    };

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(420),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTime.UtcNow.AddMinutes(420)
        };
        Response.Cookies.Append("jwt", tokenString, cookieOptions);
        return Ok(new { findUser.Id, Token = tokenString });
    }
    // DELETE api/<AuthController>
    [HttpPost("logout")]
    public IActionResult Logout()
    {
        if (Request.Cookies["jwt"] != null)
        {
            Response.Cookies.Delete("jwt");
            return Ok(new { message = "התנתקות בוצעה בהצלחה" });
        }
        return BadRequest(new { message = "המשתמש כבר מנותק" });
    }
}







