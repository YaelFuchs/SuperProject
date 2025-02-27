using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class User
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [JsonIgnore] // מונע מחזוריות
        // קשר נכון עם תפקידים
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
