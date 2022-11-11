﻿using Hms.Models;
using Hms.Models.Utility;
using Hms.Models.ViewModels;
using HMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Service
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IRepository _repository;

        public InvoiceService(IRepository repository)
        {
            _repository = repository;
        }

        // Todo : add await to every async call.
        public async Task<Invoice> AddInvoiceAsync(string userID, InvoiceViewModel invoiceViewModel)
        {
            var invoice = new Invoice()
            {
                UserId = userID,
                Status = InvoiceStatus.Pending,
                TableId = invoiceViewModel.TableId,
                InvoiceDate = invoiceViewModel.InvoiceDate,
            };
            await _repository.Create(invoice);
            await _repository.SaveAsync();

            return invoice;
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            var Invoicelist = await _repository.FindAll<Invoice>().ToListAsync();
            return Invoicelist;
        }

        public async Task<Invoice> GetInvoicebyID(int id)
        {
            var invoice = await _repository.GetByID<Invoice>(id);
            return invoice;
        }

        public async Task<List<Invoice>> GetInvoiceByUserId(string userId)
        {
            var invoicelist = await _repository.FindByCondition<Invoice>(l => l.UserId == userId).ToListAsync();
            return invoicelist;
        }

        public async Task<Invoice> UpdateInvoiceAsync(int id, string UserId, InvoiceViewModel invoiceViewModel)
        {
            var invoice = new Invoice()
            {
                InvoiceId = id,
                UserId = UserId,
                Status = InvoiceStatus.Pending,
                TableId=invoiceViewModel.TableId,
                InvoiceDate = invoiceViewModel.InvoiceDate
            };
            _repository.Update(invoice);
            await _repository.SaveAsync();
            return invoice;
        }

        public bool Delete(Invoice invoice)
        {
            if (invoice != null)
            {
                _repository.Delete(invoice);
                _repository.Save();
                return true;
            }

            return false;
        }
    }

    public interface IInvoiceService
    {
        Task<Invoice> AddInvoiceAsync(string userID, InvoiceViewModel invoiceViewModel);
        Task<List<Invoice>> GetAllInvoices();
        Task<Invoice> GetInvoicebyID(int id);
        Task<List<Invoice>> GetInvoiceByUserId(string userId);
        Task<Invoice> UpdateInvoiceAsync(int id, string UserId, InvoiceViewModel invoiceViewModel);
        bool Delete(Invoice invoice);
    } 
}