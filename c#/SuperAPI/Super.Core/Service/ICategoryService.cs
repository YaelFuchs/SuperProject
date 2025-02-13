using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Service
{
    public interface ICategoryService
    {
        public List<Category> GetAllCategories();
        public Category GetCategoryById(int id);
        public void AddCategory(Category category);
        public void UpdateCategory(int Id, Category category);
        public void DeleteCategory(int Id);
    }
}
