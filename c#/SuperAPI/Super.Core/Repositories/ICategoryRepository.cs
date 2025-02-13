using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Repositories
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategoryList();
        public Category GetCategoryById(int Id);
        public void AddCategory(Category category);
        public void UpdateCategory(int Id, Category category);
        public void DeleteCategory(int Id);
    }
}
