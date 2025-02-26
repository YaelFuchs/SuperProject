using BCrypt.Net;
using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public List<User> GetList()
        {
            return _context.Users.ToList();
        }
        public User GetUserById(int Id)
        {
            var user = _context.Users.Find(Id);
            if(user != null)
            {
                return user;
            }
            return null;
        }
        public void SignUp(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName);

            if (existingUser == null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, workFactor: 8);
                Console.WriteLine($"Creating user: {user.UserName} with hashed password: {hashedPassword}");

                user.Password = hashedPassword; // לשמור את הסיסמה המוצפנת במסד הנתונים
                _context.Users.Add(user);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception();

            }
        }
        public void UpdateUser(int Id, User user)
        {
            var userToUpdate = _context.Users.Find(Id);
            if (userToUpdate != null)
            {
              
                
                    userToUpdate.UserName = user.UserName;
              
                    userToUpdate.Email = user.Email;
              
                    userToUpdate.Password = user.Password;
               
                _context.SaveChanges();

            }

        }
        public void DeleteUser(int Id)
        {
            var userToRemove = _context.Users.Find(Id);
            if (userToRemove != null)
            {
                _context.Users.Remove(userToRemove);
                _context.SaveChanges();
            }
        }


        public User LogIn(User user)
        {
            var findUser = _context.Users.FirstOrDefault(x => x.UserName == user.UserName);
            if (findUser != null)
            {
                if (findUser.Password == user.Password)
                {
                    return user;
                }
                else
                    throw new Exception();
            }
            else
            {
                throw new Exception();
            }
        }
        public User GetUserByName(string Name)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == Name);
        }

    }
}
