using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddTable([FromBody] TableViewModel tableViewModel)
        {
            var result = await _tableService.AddTableAsync(tableViewModel);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("Tables")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _tableService.GetAllTables();
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet("Table/{id}")]
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            var result = await _tableService.GetTablebyID(id);
            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateTable([FromRoute]int id, [FromBody] TableViewModel tableViewModel)
        {
            var result = await _tableService.UpdateTableAsync(id,tableViewModel);
            if (result)
            {
                return Ok(tableViewModel);
            }
            return BadRequest();
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTable([FromRoute] int id)
        {
            var table = await _tableService.GetTablebyID(id);
            var result = _tableService.Delete(table);
            if (result)
            {
                return Ok($"table with {id} is deleted");
            }
            return BadRequest();
        }
    }
}
