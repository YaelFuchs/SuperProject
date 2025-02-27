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
        public string Name { get; set; } = string.Empty;
        // קשר many-to-many עם משתמשים
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }

}
