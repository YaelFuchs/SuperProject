using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.DTOs;
using Super.Core.Models;
using Super.Core.Service;
using SuperAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    [Authorize(Roles = "ROLE_USER")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        // GET: api/<UsersController>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();
            //לאחר הבדיקות אפשר להחזיר את זה לDTO
            //var usersDto=_mapper.Map<List<UserDto>>(users);
            //return Ok(usersDto);
            return Ok(users);
        }

        // GET api/<UsersController>/5
        [AllowAnonymous]

        [HttpGet("{Id}")]
        public UserDto GetUserById(int Id)
        {
            var user= _userService.GetUserById(Id);
            var userDto=_mapper.Map<UserDto>(user);
            return userDto;
        }

        //POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        public void SignUp([FromBody] UserPostModel user)
        {
            _userService.SignUp(_mapper.Map<User>(user));
        }


        // PUT api/<UsersController>/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] User user)
        {
            _userService.UpdateUser(Id, user);
        }

        // DELETE api/<UsersController>/5
        [AllowAnonymous]

        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _userService.DeleteUser(Id);
        }
    }
}
