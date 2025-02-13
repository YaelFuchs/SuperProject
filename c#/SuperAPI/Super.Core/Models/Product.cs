using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Models
{
    public enum eUnitOfMeasure // הגדרת ה-enum מחוץ למחלקה
    {
        Units, // נמכר ביחידות
        Kilograms // נמכר בק"ג
    }
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public eUnitOfMeasure UnitOfMeasure { get; set; }
        public List <BranchProduct> ListOfProductBranch {get ; set;}

    }
}
