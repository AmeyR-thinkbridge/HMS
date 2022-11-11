using AutoMapper.Internal;
using Hms.Models.ViewModels;
using Hms.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;

namespace HMS._1._0.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRecordsService _invoiceRecordsService;
        private readonly IInvoiceService _invoiceService;
        private readonly IDishService _dishService;

        public InvoiceController(IInvoiceRecordsService invoiceRecordsService,
                                 IInvoiceService invoiceService,
                                 IDishService dishService)
        {
            _invoiceRecordsService = invoiceRecordsService;
            _invoiceService = invoiceService;
            _dishService = dishService;
        }

        [HttpPost("{userId}/GenerateInvoice")]
        public async Task<IActionResult> AddInvoiceAndRecords([FromRoute] string userId, [FromBody] InvoiceViewModel invoiceViewModel)
        {
            var invResult = await _invoiceService.AddInvoiceAsync(userId, invoiceViewModel);

            foreach (var record in invoiceViewModel.InvoiceRecords)
            {
                record.InvoiceId = invResult.InvoiceId;
                record.Total = _dishService.GetDishMrpByID(record.DishId) * record.Units;
            }
            var invRecordResult = await _invoiceRecordsService.AddInvoiceRecordAsync(invoiceViewModel.InvoiceRecords);

            if (invResult != null && invRecordResult)
            {
                return Ok("Invoice has been Generated");
            }
            return BadRequest("Recheck your Invoice");
        }

        //Todo : give name UpdateInvoiceAndRecords in the route and accept fields from body.

        [HttpPut("{userId}/{InvoiceId}/Update")]
        public async Task<IActionResult> UpdateInvoiceAndRecords([FromRoute] string userId,[FromRoute] int InvoiceId, [FromBody] InvoiceViewModel invoiceViewModel)
        {
            var invResult = await _invoiceService.UpdateInvoiceAsync(InvoiceId, userId, invoiceViewModel);
            var lstInvoiceRecord = _invoiceRecordsService.GetInvoiceRecordsByInvoiceId(InvoiceId);
            foreach (var record in lstInvoiceRecord)
            {
                _invoiceRecordsService.Delete(record);
            }
            foreach (var record in invoiceViewModel.InvoiceRecords)
            {
                record.InvoiceId = invResult.InvoiceId;
                record.Total = _dishService.GetDishMrpByID(record.DishId) * record.Units;
            }
            var invRecordResult = await _invoiceRecordsService.AddInvoiceRecordAsync(invoiceViewModel.InvoiceRecords);

            if (invResult != null && invRecordResult)
            {
                return Ok("Invoice has been Updated");
            }
            return BadRequest("Recheck your Invoice");
        }

        [HttpDelete("DeleteInvoice")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var invoice = await _invoiceService.GetInvoicebyID(id);
            var result = _invoiceService.Delete(invoice);
            if (result)
            {
                return Ok($"Invoice with id {id} is deleted");
            }
            return BadRequest();
        }
    }
}
