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
        public async Task<IActionResult> IncomeByDate(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.GetIncomeForDate(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("VisitsPerDay")]
        public async Task<IActionResult> VisitsPerDay(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.VisitsPerDay(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("DishesServedPerDay")]
        public async Task<IActionResult> DishesServedPerDay(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.DishesServedPerDay(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("BusiestTablePerDate")]
        public async Task<IActionResult> BusiestTablePerDate(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.BusiestTablePerDate(starDate, endDate);

            return Ok(res);
        }
        [HttpPost("BusiestHoursPerDay")]
        public async Task<IEnumerable> BusiestHoursPerDay(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.BusiestHoursPerDay(starDate, endDate);

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

        [HttpGet("CostliestDishByCategory")]
        public async Task<IActionResult> CostliestDishByCategory()
        {
            var res = await _reportService.CostliestDishByCategory();
            return Ok(res);
        }

        [HttpGet("CheapestDish")]
        public async Task<IActionResult> CheapestDish()
        {
            var res = await _reportService.CheapestDish();
            return Ok(res);
        }

        [HttpGet("CheapestDishByCategory")]
        public async Task<IActionResult> CheapestDishByCategory()
        {
            var res = await _reportService.CheapestDishByCategory();
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
            return await _reportService.FeedbackByRatings();
        }

        [HttpPost("MaxInvoiceBillPerDate")]
        public async Task<IActionResult> MaxInvoiceBillPerDate(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.MaxInvoiceBillPerDate(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("MinInvoiceBillPerDate")]
        public async Task<IActionResult> MinInvoiceBillPerDate(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.MinInvoiceBillPerDate(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("AvgInvoiceBillPerDate")]
        public async Task<IActionResult> AvgInvoiceBillPerDate(DateTime starDate, DateTime endDate)
        {
            starDate = starDate.Date;
            endDate = endDate.Date;
            var res = await _reportService.AvgInvoiceBillPerDate(starDate, endDate);

            return Ok(res);
        }

        [HttpPost("VisitPerDay2")]
        public async Task<IActionResult> VisitPerDay2(DateTime startdate, DateTime endDate)
        {
            startdate = startdate.Date;
            endDate = endDate.Date;

            var res = await _reportService.VisitPerDay2(startdate, endDate);

            return Ok(res);
        }
    }
}
