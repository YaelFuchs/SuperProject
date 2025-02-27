using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Super.Core.DTOs;
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
            var users = _context.Users.ToList();

            return users;
        }
        public User GetUserById(int Id)
        {
            var user = _context.Users.Find(Id);
            if (user != null)
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
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, salt);
                user.Password = hashedPassword;

                var userRole = _context.Roles.FirstOrDefault(r => r.Name == ERole.ROLE_USER);
                if (userRole == null)
                {
                    userRole = new Role { Name = ERole.ROLE_USER };
                    _context.Roles.Add(userRole);
                    _context.SaveChanges();
                }

                user.UserRoles.Add(new UserRole { User = user, Role = userRole });

                if (user.UserName.StartsWith("manager"))
                {
                    var adminRole = _context.Roles.FirstOrDefault(r => r.Name == ERole.ROLE_ADMIN);
                    if (adminRole == null)
                    {
                        adminRole = new Role { Name = ERole.ROLE_ADMIN };
                        _context.Roles.Add(adminRole);
                        _context.SaveChanges();
                    }
                    user.UserRoles.Add(new UserRole { User = user, Role = adminRole });
                }

                if (user.UserName == "manager1234")
                {
                    var managerRole = _context.Roles.FirstOrDefault(r => r.Name == ERole.ROLE_MANAGER);
                    if (managerRole == null)
                    {
                        managerRole = new Role { Name = ERole.ROLE_MANAGER };
                        _context.Roles.Add(managerRole);
                        _context.SaveChanges();
                    }
                    user.UserRoles.Add(new UserRole { User = user, Role = managerRole });
                }
                _context.Users.Add(user);
                _context.SaveChanges();
                // ✅ בדיקה אם נוספו התפקידים
                Console.WriteLine($"User {user.UserName} has the following roles:");
                foreach (var u in user.UserRoles)
                {
                    Console.WriteLine($"- {u.Role.Name}");
                }
            }
            else
            {
                throw new Exception("User already exists!");
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
            var user = _context.Users
            .Include(u => u.UserRoles)
            .FirstOrDefault(u => u.Id == Id);

            if (user != null)
            {
                // מחיקת כל הקשרים בין המשתמש לתפקידים
                _context.UserRoles.RemoveRange(user.UserRoles);
                _context.SaveChanges(); // שים לב ששמרנו את השינויים

                // עכשיו ניתן למחוק את המשתמש
                _context.Users.Remove(user);
                _context.SaveChanges(); // שמירת המחיקה
            }
            else
            {
                throw new Exception("User not found");
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
