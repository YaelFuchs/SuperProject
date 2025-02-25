using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface IUserRepository
    {
        public List<User> GetList();
        public User GetUserById(int Id);
        public void SignUp(User user);
        public void UpdateUser(int Id, User user);
        public void DeleteUser(int Id);
        public User LogIn(User user);
        public User GetUserByName(string Name);
    }
}
