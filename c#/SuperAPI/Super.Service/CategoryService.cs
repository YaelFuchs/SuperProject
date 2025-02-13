using Super.Core.Models;
using Super.Core.Repositories;
using Super.Core.Service;
using Super.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Service
{
    public class CategoryService:ICategoryService

    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public List<Category> GetAllCategories()
        {
            return _categoryRepository.GetCategoryList();
        }
        public Category GetCategoryById(int Id)
        {
            return _categoryRepository.GetCategoryById(Id);

        }
        public void AddCategory(Category category)
        {
            _categoryRepository.AddCategory(category);
        }
        public void UpdateCategory(int Id, Category category)
        {
            _categoryRepository.UpdateCategory(Id, category);
        }

        public void DeleteCategory(int Id)
        {
            _categoryRepository.DeleteCategory(Id);
        }
    }
}
