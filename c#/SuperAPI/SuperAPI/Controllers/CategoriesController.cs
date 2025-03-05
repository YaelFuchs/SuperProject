using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Super.Core;
using Super.Core.Models;
using Super.Core.Service;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "User")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoriesController>
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Category> GetAllCategories()
        {
            return _categoryService.GetAllCategories();
        }

        // GET api/<CategoriesController>/5
        [HttpGet("{Id}")]
        [AllowAnonymous]

        public Category GetCategoryById(int Id)
        {
            return _categoryService.GetCategoryById(Id);
        }

        // POST api/<CategoriesController>
        //[Authorize(Policy = "Manager")]
        [AllowAnonymous]

        [HttpPost]
        public void AddCategory([FromBody] Category category)
        {
            _categoryService.AddCategory(category);
        }

        // PUT api/<CategoriesController>/5
        [Authorize(Policy = "Manager")]
        //[AllowAnonymous]

        [HttpPut("{Id}")]
        public void UpdateCategory(int Id, [FromBody] Category category)
        {
            _categoryService.UpdateCategory(Id, category);
        }

        // DELETE api/<CategoriesController>/5
        [Authorize(Policy = "Manager")]
        [HttpDelete("{Id}")]
        public void DeleteCategory(int Id)
        {
            _categoryService.DeleteCategory(Id);
        }
    }
}
