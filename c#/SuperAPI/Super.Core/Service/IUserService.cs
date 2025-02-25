using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface IUserService
    {
        public List<User> GetAllUsers();
        public User GetUserById(int id);
        public void SignUp(User user);
        public void UpdateUser(int Id, User user);
        public void DeleteUser(int Id);
        public User LogIn(User user);
        public User GetUserByName(string Name);

    }
}
