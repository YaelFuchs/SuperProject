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
               string salt = BCrypt.Net.BCrypt.GenerateSalt(12);  // או כל רמת קושי אחרת
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt); // הצפנת הסיסמה עם ה-salt החדש
        Console.WriteLine($"Creating user: {user.UserName} with hashed password: {hashedPassword}");

        user.Password = hashedPassword; // לשמור את הסיסמה המוצפנת במסד הנתונים

                var userRole = _context.Roles.FirstOrDefault(r => r.Name == ERole.ROLE_USER);
                if (userRole == null)
                {
                    user.Roles = new List<Role>();

                    userRole = new Role { Name = ERole.ROLE_USER };
                    _context.Roles.Add(userRole);
                }

                user.Roles.Add(userRole); // הוספת התפקיד לרשימת התפקידים של המשתמש

                if (user.UserName.StartsWith("Manager"))//אם השם משתמש מתחיל במחרוזת מנהל תוסיף תפקיד מנהל להרשאות
                {
                    var adminRole = new Role
                    {
                        Name = ERole.ROLE_ADMIN 
                    };
                    
                    user.Roles.Add(adminRole);
                  
                }
                if (user.UserName == "Manager1234")//אם השם משתמש שלך שווה לזה תוסיף הרשאת מנהל ראשי להרשאות
                {
                  
                    var managerRole = new Role
                    {
                        Name = ERole.ROLE_MANAGER
                    };
                    
                    user.Roles.Add(managerRole);
               
                }

                _context.Users.Add(user);
                Console.WriteLine($"Roles for {user.UserName}: {string.Join(", ", user.Roles.Select(r => r.Name))}");

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
