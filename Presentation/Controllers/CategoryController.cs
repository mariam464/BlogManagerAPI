using Business.ServicesLayer.Dtos;
using Business.ServicesLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            if (id > 0)
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);
            }
            else
            {
                return BadRequest("Invalid id");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category = await _categoryService.AddCategoryAsync(dto);
                return Ok(category);
            }
            else
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, CreateCategoryDto dto)
        {
            if (id > 0)
            {
                if (ModelState.IsValid)
                {
                    var category = await _categoryService.UpdateCategoryAsync(id, dto);
                    return Ok(category);
                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id > 0)
            {
                await _categoryService.DeleteCategoryAsync(id);
                return NoContent();
            }
            else
            {
                return BadRequest("Invalid Id");
            }
        }
    }
}
