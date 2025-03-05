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
        // חפש את המשתמש במסד הנתונים
        var findUser = _userService.GetUserByName(userLoginModel.UserName);
        if (findUser == null)
        {
            return BadRequest(new { message = "User not found" });
        }
        // הדפסת ה-UserRoles כדי לוודא שהם נטענים
        // בדוק אם הסיסמה נכונה
        else if (!BCrypt.Net.BCrypt.Verify(userLoginModel.Password, findUser.Password))
        {
            return Unauthorized(new { message = "Incorrect password" });
        }
        var roles = findUser.UserRoles?.Select(ur => ur.Role.Name).ToList() ?? new List<string>();
        // צור רשימה של Claims, כולל שם המשתמש ותפקידים
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, findUser.UserName),
            new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", string.Join(",", roles)) // שינוי כאן
    };

        // קח את המפתח הסודי מהקונפיגורציה
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWT:Key")));
        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        // צור את הטוקן
        var tokenOptions = new JwtSecurityToken(
            issuer: _configuration.GetValue<string>("JWT:Issuer"),
            audience: _configuration.GetValue<string>("JWT:Audience"),
            claims: claims,
            expires: DateTime.Now.AddMinutes(420), // טווח הזמן של הטוקן (כאן הוא מוגדר לשעה)
            signingCredentials: signinCredentials
        );

        // הפוך את הטוקן ל-String
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        // החזר את הטוקן כתגובה
        return Ok(new { findUser.Id, Token = tokenString });
    }
}







