using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{


    public class Role
    {
        public int Id { get; set; }
        public  ERole Name { get; set; }
        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
