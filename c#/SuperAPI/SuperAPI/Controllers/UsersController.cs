using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.Models;
using Super.Core.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<User> GetAllUsers()
        {
            return _userService.GetAllUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{Id}")]
        public User GetUserById(int Id)
        {
            return _userService.GetUserById(Id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] User user)
        {
            _userService.AddUser(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{Id}")]
        public void Put(int Id, [FromBody] User user)
        {
            _userService.UpdateUser(Id, user);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{Id}")]
        public void Delete(int Id)
        {
            _userService.DeleteUser(Id);
        }
    }
}
