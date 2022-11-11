using HMS.Data.Repository;
using Hms.Models.Utility;
using Hms.Models.ViewModels;
using Hms.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Hms.Service
{
    public class InvoiceRecordsService :  IInvoiceRecordsService
    {
        private readonly IRepository _repository;

        public InvoiceRecordsService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> AddInvoiceRecordAsync(List<InvoiceRecordsViewModel> lstinvoiceRecordsViewModel)
        {
            int listitems = lstinvoiceRecordsViewModel.Count;
            foreach (var record in lstinvoiceRecordsViewModel)
            {
                var invoiceRecord = new InvoiceRecords()
                {
                    DishId = record.DishId,
                    Units = record.Units,
                    Total = record.Total,
                    InvoiceId = record.InvoiceId,

                };
                await _repository.Create(invoiceRecord);
                await _repository.SaveAsync();
                --listitems;
            }
            if (listitems == 0)
            {
                return true;
            }
            return false;
        }

        public Task<List<InvoiceRecords>> GetAllInvoiceRecords()
        {
            var InvoiceRecordlist = _repository.FindAll<InvoiceRecords>().ToListAsync();
            return InvoiceRecordlist;
        }

        public async Task<InvoiceRecords> GetInvoiceReccordbyID(int id)
        {
            var invoicerecord = await _repository.GetByID<InvoiceRecords>(id);
            return invoicerecord;
        }

        public List<InvoiceRecords> GetInvoiceRecordsByInvoiceId(int invoiceId)
        {
            var invoicelist = _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == invoiceId).ToList();
            return invoicelist;
        }

        public async Task<bool> UpdateInvoiceRecordAsync(int id, InvoiceRecordsViewModel invoiceRecordsViewModel)
        {
            var invoiceRecord = new InvoiceRecords()
            {
                InvoiceRecordId = id,
                InvoiceId = invoiceRecordsViewModel.InvoiceId,
                DishId = invoiceRecordsViewModel.DishId,
                Units = invoiceRecordsViewModel.Units,
                Total = invoiceRecordsViewModel.Total
            };
            _repository.Update(invoiceRecord);
            await _repository.SaveAsync();
            return true;
        }

        public bool Delete(InvoiceRecords invoiceRecord)
        {
            if (invoiceRecord != null)
            {
                _repository.Delete(invoiceRecord);
                _repository.Save();
                return true;
            }

            return false;
        }
    }

    public interface IInvoiceRecordsService
    {
        Task<bool> AddInvoiceRecordAsync(List<InvoiceRecordsViewModel> lstinvoiceRecordsViewModel);
        Task<List<InvoiceRecords>> GetAllInvoiceRecords();
        Task<InvoiceRecords> GetInvoiceReccordbyID(int id);
        List<InvoiceRecords> GetInvoiceRecordsByInvoiceId(int invoiceId);
        Task<bool> UpdateInvoiceRecordAsync(int id, InvoiceRecordsViewModel invoiceRecordsViewModel);
        bool Delete(InvoiceRecords invoiceRecord);
    }
}
