using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        //public string City { get; set; }
        public string Email { get; set; }

        //אנחנו צריכות להוסיף רשימה מסוג מוצר סניף עם קשרי גומלין

        public List<BranchProduct> BranchProducts { get; set; }
    }
}
