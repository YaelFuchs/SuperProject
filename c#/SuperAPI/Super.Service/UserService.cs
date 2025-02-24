using Super.Core.Models;
using Super.Core.Repositories;
using Super.Core.Service;
using Super.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Service
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<User> GetAllUsers()
        {
            return _userRepository.GetList();
        }
        public User GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);

        }
        public void SignUp(User user)
        {
            _userRepository.SignUp(user);
        }
        public void UpdateUser(int Id, User user)
        {
            _userRepository.UpdateUser(Id, user);
        }

        public void DeleteUser(int Id)
        {
            _userRepository.DeleteUser(Id);
        }
        public User LogIn(User user)
        {
            return _userRepository.LogIn(user);
        }
    }
}
