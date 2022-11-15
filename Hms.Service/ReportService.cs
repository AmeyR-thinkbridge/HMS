using Hms.Models;
using Hms.Models.Utility;
using HMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Service
{
    public class ReportService : IReportService
    {
        private readonly IRepository _repository;

        public ReportService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<double> GetIncomeForDate(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            List<InvoiceRecords> invRecords = new List<InvoiceRecords>();
            foreach (var record in invoicesForToday)
            {
                invRecords.AddRange(await _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == record.InvoiceId).ToListAsync());
            }
            var list = from i in invoicesForToday
                       join r in invRecords on i.InvoiceId equals r.InvoiceId
                       select new
                       {
                           Total = r.Total,
                       };

            return list.Sum(l => l.Total.Value);

        }

        public async Task<double> DishesServedPerDay(DateTime date)
        {
            //ToDo : Return list of dishes served in a day.
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            List<InvoiceRecords> invRecords = new List<InvoiceRecords>();
            foreach (var record in invoicesForToday)
            {
                invRecords.AddRange(await _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == record.InvoiceId).ToListAsync());
            }
            var list = from i in invoicesForToday
                       join r in invRecords on i.InvoiceId equals r.InvoiceId
                       select new
                       {
                           Units = r.Units
                       };
            return list.Sum(l => l.Units);
        }

        public async Task<string> BusiestTablePerDate(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            var tablelist = from i in invoicesForToday
                            group i by i.TableId into tg
                            select new
                            {
                                table = tg.Key,
                                Count = tg.Sum(l => l.InvoiceId)
                            };
            var tableid = tablelist.MaxBy(t => t.Count);
            var table = await _repository.GetByID<Table>(tableid.table.Value);
            return table.Description;
        }

        public async Task<int> VisitsPerDay(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            return invoicesForToday.Count;
        }

        public async Task<string> MostOrderedDish()
        {
            var invrecords = await _repository.FindAll<InvoiceRecords>().ToListAsync();
            var invRecList = from i in invrecords
                             group i by i.DishId into irl
                             select
                             (
                                 DishId: irl.Key,
                                 Count: (from o in irl
                                         select o.Units).Sum()
                             );

            var record = invRecList.OrderByDescending(l => l.Count).First();

            var rec = await _repository.GetByID<Dish>(record.DishId.Value);

            return rec.Name;
        }

        public async Task<string> CostliestDish()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var dish = dishlst.MaxBy(l => l.MRP);

            return dish.Name;
        }
        public async Task<IEnumerable> CostliestDishByCategory()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var categoryList = await _repository.FindAll<DishCategroy>().ToListAsync();
            var dishbycategories = from d in dishlst
                                   join c in categoryList on d.DishCategroyId equals c.CategoryId
                                   group d by c.Description into cdg
                                   select new
                                   {
                                       Category = cdg.Key,
                                       Dishes = cdg.OrderByDescending(l=>l.MRP)
                                   };

            return dishbycategories;
        }

        public async Task<string> CheapestDish()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var dish = dishlst.MinBy(l => l.MRP);

            return dish.Name;
        }

        public async Task<IEnumerable> CheapestDishByCategory()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var categoryList = await _repository.FindAll<DishCategroy>().ToListAsync();
            var dishbycategories = from d in dishlst
                                   join c in categoryList on d.DishCategroyId equals c.CategoryId
                                   group d by c.Description into cdg
                                   select new
                                   {
                                       Category = cdg.Key,
                                       Dishes = cdg.OrderBy(l => l.MRP)
                                   };

            return dishbycategories;
        }

        public async Task<int> TotalNumberOfCustomerVisits()
        {
            var invoicelst = await _repository.FindAll<Invoice>().ToListAsync();

            return invoicelst.Count;
        }

        public async Task<double> MaxInvoiceBillPerDate(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            List<InvoiceRecords> invRecords = new List<InvoiceRecords>();
            foreach (var record in invoicesForToday)
            {
                invRecords.AddRange(await _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == record.InvoiceId).ToListAsync());
            }
            var list = from i in invoicesForToday
                       join r in invRecords on i.InvoiceId equals r.InvoiceId
                       group r by r.InvoiceId into irl
                       select new
                       {
                           Invoice = irl.Select(l => l.InvoiceId),
                           Total = irl.Sum(l => l.Total.Value)
                       };

            return list.Max(l => l.Total);
        }

        public async Task<double> MinInvoiceBillPerDate(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            List<InvoiceRecords> invRecords = new List<InvoiceRecords>();
            foreach (var record in invoicesForToday)
            {
                invRecords.AddRange(await _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == record.InvoiceId).ToListAsync());
            }
            var list = from i in invoicesForToday
                       join r in invRecords on i.InvoiceId equals r.InvoiceId
                       group r by r.InvoiceId into irl
                       select new
                       {
                           Invoice = irl.Select(l => l.InvoiceId),
                           Total = irl.Sum(l => l.Total.Value)
                       };

            return list.Min(l => l.Total);
        }

        public async Task<double> AvgInvoiceBillPerDate(DateTime date)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date.Equals(date)).ToListAsync();
            List<InvoiceRecords> invRecords = new List<InvoiceRecords>();
            foreach (var record in invoicesForToday)
            {
                invRecords.AddRange(await _repository.FindByCondition<InvoiceRecords>(l => l.InvoiceId == record.InvoiceId).ToListAsync());
            }
            var list = from i in invoicesForToday
                       join r in invRecords on i.InvoiceId equals r.InvoiceId
                       group r by r.InvoiceId into irl
                       select new
                       {
                           Invoice = irl.Select(l => l.InvoiceId),
                           Total = irl.Sum(l => l.Total.Value)
                       };

            return list.Average(l => l.Total);
        }

        public async Task<IEnumerable> BusiestHoursPerDay(DateTime date)
        {
            var invoiceList = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date == date).ToListAsync();
            var list = from i in invoiceList
                       group i by i.InvoiceDate.Date into dg
                       select new
                       {
                           InvoiceDate = dg.Key,
                           InvoiceHour = from i in dg
                                         group i by i.InvoiceDate.Hour into hg
                                         select new
                                         {
                                             Hour = hg.Key,
                                             Invoice = hg
                                         }
                       };
            return list;
        }

        public async Task<IEnumerable> InvoiceStatusReport()
        {
            var invlist = await _repository.FindAll<Invoice>().ToListAsync();
            var list = from i in invlist
                       group i by i.Status into sg
                       select new
                       {
                           InvoiceStatus = sg.Key,
                           InvoiceCount = sg.Count()
                       };
            return list;
        }

        public async Task<IEnumerable> OrdersByCategory()
        {
            var invRecList = await _repository.FindAll<InvoiceRecords>().ToListAsync();
            var dishlist = await _repository.FindAll<Dish>().ToListAsync();
            var categoryList = await _repository.FindAll<DishCategroy>().ToListAsync();

            var list = from c in categoryList
                       join d in dishlist on c.CategoryId equals d.DishCategroyId
                       join i in invRecList on d.DishId equals i.DishId
                       group i by c.Description into ls
                       select new
                       {
                           Category = ls.Key,
                           Count = ls.Sum(l => l.Units)
                       };


            return list;
        }

        public async Task<IEnumerable> OrdersByDishAndCategory()
        {
            var invRecList = await _repository.FindAll<InvoiceRecords>().ToListAsync();
            var dishlist = await _repository.FindAll<Dish>().ToListAsync();
            var categoryList = await _repository.FindAll<DishCategroy>().ToListAsync();

            var list = from c in categoryList
                       join d in dishlist on c.CategoryId equals d.DishCategroyId
                       group d by c.Description into ls
                       select new
                       {
                           Catgory = ls.Key,
                           Dishes = from l in ls
                                    join i in invRecList on l.DishId equals i.DishId
                                    group i by l.Name into lsn
                                    select new
                                    {
                                        Dish = lsn.Key,
                                        Count = lsn.Sum(l => l.Units)
                                    }
                       };

            return list;
        }

        public async Task<IEnumerable> FeedbackCountByCategory()
        {
            var feedbacklist = await _repository.FindAll<FeedBack>().ToListAsync();
            var list = from f in feedbacklist
                       group f by f.Description into lst
                       select new
                       {
                           Stars = lst.Key,
                           Count = lst.Count()
                       };

            return list;
        }
    }
    public interface IReportService
    {
        Task<double> GetIncomeForDate(DateTime date);
        Task<int> VisitsPerDay(DateTime date);
        Task<double> DishesServedPerDay(DateTime date);
        Task<string> BusiestTablePerDate(DateTime date);
        Task<IEnumerable> BusiestHoursPerDay(DateTime date);


        Task<int> TotalNumberOfCustomerVisits();
        Task<string> MostOrderedDish();
        Task<string> CostliestDish();
        Task<IEnumerable> CostliestDishByCategory();
        Task<string> CheapestDish();
        Task<IEnumerable> CheapestDishByCategory();
        Task<IEnumerable> InvoiceStatusReport();
        Task<IEnumerable> OrdersByCategory();
        Task<IEnumerable> OrdersByDishAndCategory();
        Task<IEnumerable> FeedbackCountByCategory();


        Task<double> MaxInvoiceBillPerDate(DateTime date);
        Task<double> MinInvoiceBillPerDate(DateTime date);
        Task<double> AvgInvoiceBillPerDate(DateTime date);
    }
}
