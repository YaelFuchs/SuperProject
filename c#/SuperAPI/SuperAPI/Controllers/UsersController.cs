using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Models;

namespace SuperAPI.Controllers
{
    [Authorize(Policy = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        // GET: api/<UsersController>
        [Authorize(Policy = "Manager")]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            //לאחר הבדיקות אפשר להחזיר את זה לDTO
            var usersDto = _mapper.Map<List<UserDto>>(users);
            return Ok(usersDto);
        }

        // GET api/<UsersController>/5
        [HttpGet("{Id}")]
        public ActionResult GetUserById(int Id)
        {
            try
            {
                var user = _userService.GetUserById(Id);
                var userDto = _mapper.Map<UserDto>(user);
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest("problem with fetch the data");
            }
        }
        //POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignUp([FromBody] UserPostModel user)
        {
            try
            {
                _userService.SignUp(_mapper.Map<User>(user));
                return Ok(new { message = "User registered successfully" }); // החזרת תגובה מוצלחת
            }
            catch (Exception ex)
            {
                if (ex.Message == "User already exists!")
                {
                    return Conflict(new { error = "User already exists!" }); 
                }
                return BadRequest(new { error = ex.Message }); 
            }
        }
        // PUT api/<UsersController>/5
        [HttpPut("{Id}")]
        public IActionResult Put(int Id, [FromBody] UserPostModel user)
        {
            try
            {
                _userService.UpdateUser(Id, _mapper.Map<User>(user));
                return Ok(new { message = "update successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest("error");
            }
        }
        // DELETE api/<UsersController>/5
        [HttpDelete("{Id}")]
        public ActionResult Delete(int Id)
        {
            try
            {
                var userClaims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
                Console.WriteLine("Claims received from token:");
                foreach (var claim in userClaims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
                _userService.DeleteUser(Id);
                return Ok(new { message = "User deleted successfully" });
            }
            catch
            {
                return BadRequest("problem with the delete");
            }
        }
    }
}
