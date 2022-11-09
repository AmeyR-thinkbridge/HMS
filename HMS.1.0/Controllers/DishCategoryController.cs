using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishCategoryController : ControllerBase
    {
        private readonly IDishCategoryService _dishCategoryService;

        public DishCategoryController(IDishCategoryService dishCategoryService)
        {
            _dishCategoryService = dishCategoryService;
        }
        [HttpPost("AddDishCategory")]
        public async Task<IActionResult> AddDishCategory([FromBody] DishCategoryViewModel dishCategoryViewModel)
        {
            var result = await _dishCategoryService.AddDishCategoryAsync(dishCategoryViewModel);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
        [HttpGet("DishCategories")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dishCategoryService.GetAllDishCategories();
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("DishCategory/{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var result = await _dishCategoryService.GetDishCategorybyID(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDishCategory([FromRoute] int id, [FromBody] DishCategoryViewModel dishCategoryViewModel)
        {
            var result = await _dishCategoryService.UpdateDishCategoryAsync(id, dishCategoryViewModel);
            if (result)
            {
                return Ok(dishCategoryViewModel);
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDishCategory([FromRoute] int id)
        {
            var dishCategroy = await _dishCategoryService.GetDishCategorybyID(id);
            var result = _dishCategoryService.Delete(dishCategroy);
            if (result)
            {
                return Ok($"table with {id} is deleted");
            }
            return BadRequest();
        }
    }
}
