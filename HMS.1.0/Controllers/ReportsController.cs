using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("IncomeByDate")]
        public async Task<IActionResult> IncomeByDate([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.GetIncomeForDate(date);

            return Ok(res);
        }

        [HttpPost("VisitsPerDay")]
        public async Task<IActionResult> VisitsPerDay([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.VisitsPerDay(date);

            return Ok(res);
        }

        [HttpPost("DishesServedPerDay")]
        public async Task<IActionResult> DishesServedPerDay([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.DishesServedPerDay(date);

            return Ok(res);
        }

        [HttpPost("BusiestTablePerDate")]
        public async Task<IActionResult> BusiestTablePerDate([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.BusiestTablePerDate(date);

            return Ok(res);
        }
        [HttpPost("BusiestHoursPerDay")]
        public async Task<IEnumerable> BusiestHoursPerDay([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.BusiestHoursPerDay(date);

            return res;
        }


        [HttpGet("TotalNumberOfCustomerVisits")]
        public async Task<IActionResult> TotalNumberOfCustomerVisits()
        {
            var res = await _reportService.TotalNumberOfCustomerVisits();
            return Ok(res);
        }

        [HttpGet("MostOrderedDish")]
        public async Task<IActionResult> MostOrderedDish()
        {
            var res = await _reportService.MostOrderedDish();
            return Ok(res);
        }

        [HttpGet("CostliestDish")]
        public async Task<IActionResult> CostliestDish()
        {
            var res = await _reportService.CostliestDish();
            return Ok(res);
        }

        [HttpGet("CheapestDish")]
        public async Task<IActionResult> CheapestDish()
        {
            var res = await _reportService.CheapestDish();
            return Ok(res);
        }

        [HttpGet("InvoiceStatusReport")]
        public async Task<IEnumerable> InvoiceStatusReport()
        {
           return await _reportService.InvoiceStatusReport();
        }

        [HttpGet("OrdersByCategory")]
        public async Task<IEnumerable> OrdersByCategory()
        {
            return await _reportService.OrdersByCategory();
        }

        [HttpGet("OrdersByDishAndCategory")]
        public async Task<IEnumerable> OrdersByDishAndCategory()
        {
            return await _reportService.OrdersByDishAndCategory();
        }

        [HttpGet("FeedbackCountByCategory")]
        public async Task<IEnumerable> FeedbackCountByCategory()
        {
            return await _reportService.FeedbackCountByCategory();
        }

        [HttpPost("MaxInvoiceBillPerDate")]
        public async Task<IActionResult> MaxInvoiceBillPerDate([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.MaxInvoiceBillPerDate(date);

            return Ok(res);
        }

        [HttpPost("MinInvoiceBillPerDate")]
        public async Task<IActionResult> MinInvoiceBillPerDate([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.MinInvoiceBillPerDate(date);

            return Ok(res);
        }

        [HttpPost("AvgInvoiceBillPerDate")]
        public async Task<IActionResult> AvgInvoiceBillPerDate([FromBody] DateTime date)
        {
            date = date.Date;
            var res = await _reportService.AvgInvoiceBillPerDate(date);

            return Ok(res);
        }
    }
}
