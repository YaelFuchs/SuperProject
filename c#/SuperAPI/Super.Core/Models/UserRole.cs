using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class UserRole
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } // קשר למשתמש

        public int RoleId { get; set; }
        public Role Role { get; set; } // קשר לתפקיד
    }
}
