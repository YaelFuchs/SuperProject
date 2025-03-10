using Super.Core.Models;
using Super.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public List<Category> GetCategoryList()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategoryById(int Id)
        {
            var category = _context.Categories.Find(Id);
            if (category != null)
            {
                return category;
            }
            return null;
        }
        public void AddCategory(Category category)
        {
            // בדיקה האם קיים משתמש עם אותו שם משתמש
            var existingCategory = _context.Categories.FirstOrDefault(c => c.Name == category.Name);

            if (existingCategory == null) // אם לא קיים משתמש עם שם משתמש זהה
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

        }
        public void UpdateCategory(int Id, Category category)
        {
            var categoryToUpdate = _context.Categories.Find(Id);
            if (categoryToUpdate != null)
            {
                if (!categoryToUpdate.Name.Equals(category.Name))
                {
                    categoryToUpdate.Name = category.Name;

                }
                _context.SaveChanges();

            }

        }
        public void DeleteCategory(int Id)
        {
            var categoryToRemove = _context.Categories.Find(Id);
            if (categoryToRemove != null)
            {
                _context.Categories.Remove(categoryToRemove);
                _context.SaveChanges();
            }
        }
    }
}
