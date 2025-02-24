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
         //בדיקה האם קיים משתמש עם אותו שם משתמש
         var existingUser = _context.Users.FirstOrDefault(u => u.UserName == user.UserName
            && u.Password == user.Password);

          if (existingUser == null) // אם לא קיים משתמש עם שם משתמש זהה
        {
        _context.Users.Add(user);
         _context.SaveChanges();
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
    }
}
