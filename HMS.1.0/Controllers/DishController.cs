using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }

        //Todo : User Friendly error messages in the api response.

        [HttpPost("AddDish")]
        public async Task<IActionResult> AddDish([FromBody] DishViewModel dishViewModel)
        {
            var result = await _dishService.AddDishAsync(dishViewModel);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }
        [HttpGet("Dishes")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _dishService.GetAllDishes();
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("Dish/{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var result = await _dishService.GetDishbyID(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int id, [FromBody] DishViewModel dishViewModel)
        {
            var result = await _dishService.UpdateDishAsync(id, dishViewModel);
            if (result)
            {
                return Ok(dishViewModel);
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteDish([FromRoute] int id)
        {
            var dish = await _dishService.GetDishbyID(id);
            var result = _dishService.Delete(dish);
            if (result)
            {
                return Ok($"Dish is deleted");
            }
            return BadRequest();
        }
    }
}
