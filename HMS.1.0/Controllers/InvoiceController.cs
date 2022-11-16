﻿using AutoMapper.Internal;
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

        [HttpPost("GenerateInvoice")]
        public async Task<IActionResult> AddInvoiceAndRecords([FromBody] InvoiceViewModel invoiceViewModel)
        {
            var invResult = await _invoiceService.AddInvoiceAsync(invoiceViewModel);

            foreach (var record in invoiceViewModel.InvoiceRecords)
            {
                record.InvoiceId = invResult.InvoiceId;
                record.Total = await _dishService.GetDishMrpByID(record.DishId) * record.Units;
            }
            var invRecordResult = await _invoiceRecordsService.AddInvoiceRecordAsync(invoiceViewModel.InvoiceRecords);

            if (invResult != null && invRecordResult)
            {
                return Ok("Invoice has been Generated");
            }
            return BadRequest("Recheck your Invoice");
        }

        //Todo : give name UpdateInvoiceAndRecords in the route and accept fields from body.

        [HttpPut("UpdateInvoiceAndRecords")]
        public async Task<IActionResult> UpdateInvoiceAndRecords([FromBody] InvoiceViewModel invoiceViewModel)
        {
            var invResult = await _invoiceService.UpdateInvoiceAsync(invoiceViewModel);
            var lstInvoiceRecord = _invoiceRecordsService.GetInvoiceRecordsByInvoiceId(invoiceViewModel.Id!.Value);
            foreach (var record in lstInvoiceRecord)
            {
                _invoiceRecordsService.Delete(record);
            }
            foreach (var record in invoiceViewModel.InvoiceRecords)
            {
                record.InvoiceId = invResult.InvoiceId;
                record.Total = await _dishService.GetDishMrpByID(record.DishId) * record.Units;
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
