using Hms.Models;
using HMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Hms.Service
{
    public class ReportService : IReportService
    {
        private readonly IRepository _repository;

        public ReportService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable> GetIncomeForDate(DateTime startDate, DateTime endDate)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l=>l.InvoiceRecords).ToListAsync();
            var list = from i in invoicesForToday
                       group i.InvoiceRecords by i.InvoiceDate.Date into grp
                       select new
                       {
                           Date = grp.Key,
                           Total = grp.Sum(l=>l.Sum(l=>l.Total)),
                       };

            return list.OrderBy(l=>l.Date);
        }

        public async Task<IEnumerable> DishesServedPerDay(DateTime startDate, DateTime endDate)
        {
            //ToDo : Return list of dishes served in a day.
            var invoicesForToday = _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l => l.InvoiceRecords);
            var Irecords = invoicesForToday.SelectMany(l => l.InvoiceRecords!.Select(y=> new {DishId=y.DishId,Units=y.DishId}));
            var list = from i in Irecords
                       group i by i.DishId  into irg
                       select new
                       {
                           Dish = irg.Key,
                           Units = irg.Sum(l => l.Units)
                       };
            return await list.ToListAsync();
        }

        public async Task<IEnumerable> BusiestTablePerDate(DateTime startDate, DateTime endDate)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l=>l.Table).ToListAsync();
            var tablelist = from i in invoicesForToday
                            group i by i.InvoiceDate.Date into dg
                            select new
                            {
                                Date = dg.Key,
                                Table = from a in dg
                                        group a by a.TableId into tg
                                        select new 
                                        {
                                            TableId = tg.Key,
                                            NumberOfCustomersOnTable = tg.Count(),
                                        }
                            };
            var something = from i in tablelist
                            select new
                            {
                                Date = i.Date,
                                Table = i.Table.MaxBy(l => l.NumberOfCustomersOnTable)
                            };
            return something;
            
        }

        public async Task<int> VisitsPerDay(DateTime startDate, DateTime endDate)
        {
            var invoicesForToday = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).ToListAsync();
            return invoicesForToday.Count;
        }

        public async Task<IEnumerable> VisitPerDay2(DateTime startDate, DateTime endDate)
        {
            var list = _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate);
            var newlist = from l in list
                          group l by l.InvoiceDate.Date into ls
                          select new
                          {
                              Date = ls.Key,
                              Count = ls.Count()
                          };
            return await newlist.ToListAsync();
        }
        public async Task<string> MostOrderedDish()
        {
            var invrecords = _repository.FindAll<InvoiceRecords>();
            var invRecList = from i in invrecords
                             group i by i.DishId into irl
                             select new
                             {
                                 DishId = irl.Key,
                                 Count = (from o in irl
                                          select o.Units).Sum()
                             };

            var record = invRecList.OrderByDescending(l => l.Count).First();

            var rec = await _repository.GetByID<Dish>(record.DishId!.Value);

            return rec.Name!;
        }

        public async Task<string> CostliestDish()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var dish = dishlst.MaxBy(l => l.MRP);

            return dish!.Name!;
        }
        public async Task<IEnumerable> CostliestDishByCategory()
        {
            var dishlst = _repository.FindAll<Dish>().Include(l=>l.DishCategroy);
            var dishbycategories = from d in dishlst
                                   group d by d.DishCategroy!.Description into cdg
                                   select new
                                   {
                                       Category = cdg.Key,
                                       Dishes = cdg.OrderByDescending(l => l.MRP)
                                   };

            return await dishbycategories.ToListAsync();
        }

        public async Task<string> CheapestDish()
        {
            var dishlst = await _repository.FindAll<Dish>().ToListAsync();
            var dish = dishlst.MinBy(l => l.MRP);

            return dish!.Name!;
        }

        public async Task<IEnumerable> CheapestDishByCategory()
        {
            var dishlst = await _repository.FindAll<Dish>().Include(l=>l.DishCategroy).ToListAsync();
            var dishbycategories = from d in dishlst
                                   group d by d.DishCategroy!.Description into cdg
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

        public async Task<IEnumerable> MaxInvoiceBillPerDate(DateTime startDate, DateTime endDate)
        {
            var invList = _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l=>l.InvoiceRecords);
            var list = from i in invList
                       select new
                       {
                           Invoice = i.InvoiceId,
                           Date = i.InvoiceDate.Date,
                           InvoiceTotal = i.InvoiceRecords!.Sum(l=>l.Total!.Value)
                       };

            var newlist = from i in list
                          group i by i.Date into grp
                          select new
                          {
                              Date = grp.Key,
                              MaxTotal = grp.Max(l=>l.InvoiceTotal)
                          };

            return await newlist.ToListAsync();
        }

        public async Task<IEnumerable> MinInvoiceBillPerDate(DateTime startDate, DateTime endDate)
        {
            var invList = _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l => l.InvoiceRecords);
            var list = from i in invList
                       select new
                       {
                           Invoice = i.InvoiceId,
                           Date = i.InvoiceDate.Date,
                           InvoiceTotal = i.InvoiceRecords!.Sum(l => l.Total!.Value)
                       };

            var newlist = from i in list
                          group i by i.Date into grp
                          select new
                          {
                              Date = grp.Key,
                              MinTotal = grp.Min(l => l.InvoiceTotal)
                          };

            return await newlist.ToListAsync();
        }

        public async Task<IEnumerable> AvgInvoiceBillPerDate(DateTime startDate, DateTime endDate)
        {
            var invList = _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).Include(l => l.InvoiceRecords);
            var list = from i in invList
                       select new
                       {
                           Invoice = i.InvoiceId,
                           Date = i.InvoiceDate.Date,
                           InvoiceTotal = i.InvoiceRecords!.Sum(l => l.Total!.Value)
                       };

            var newlist = from i in list
                          group i by i.Date into grp
                          select new
                          {
                              Date = grp.Key,
                              AverageTotal = grp.Average(l => l.InvoiceTotal)
                          };

            return await newlist.ToListAsync();
        }

        public async Task<IEnumerable> BusiestHoursPerDay(DateTime startDate, DateTime endDate)
        {
            var invoiceList = await _repository.FindByCondition<Invoice>(l => l.InvoiceDate.Date >= startDate && l.InvoiceDate.Date <= endDate).ToListAsync();
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
            var invlist = _repository.FindAll<Invoice>();
            var list = from i in invlist
                       group i by i.Status into sg
                       select new
                       {
                           InvoiceStatus = sg.Key,
                           InvoiceCount = sg.Count()
                       };
            return await list.ToListAsync();
        }

        public async Task<IEnumerable> OrdersByCategory()
        {
            var invRecList = await _repository.FindAll<InvoiceRecords>().Include(l => l.Dish).Include(l => l.Dish!.DishCategroy).ToListAsync();

            var list = from c in invRecList
                       group c by c.Dish!.DishCategroy!.Description into ls
                       select new
                       {
                           Category = ls.Key,
                           Count = ls.Sum(l => l.Units)
                       };


            return list;
        }

        public async Task<IEnumerable> OrdersByDishAndCategory()
        {
            var invRecList = await _repository.FindAll<InvoiceRecords>().Include(l=>l.Dish).Include(l=>l.Dish!.DishCategroy).ToListAsync();

            var list = from c in invRecList
                       group c by c.Dish!.DishCategroy!.Description into ls
                       select new
                       {
                           Category = ls.Key,
                           Dishes = from l in ls
                                    group l by l.Dish!.Name into lsn
                                    select new
                                    {
                                        Dish = lsn.Key,
                                        Count = lsn.Sum(l => l.Units)
                                    }
                       };

            return list;
        }

        public async Task<IEnumerable> FeedbackByRatings()
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
        Task<IEnumerable> GetIncomeForDate(DateTime startDate, DateTime endDate);
        Task<int> VisitsPerDay(DateTime startDate, DateTime endDate);
        Task<IEnumerable> VisitPerDay2(DateTime startDate, DateTime endDate);
        Task<IEnumerable> DishesServedPerDay(DateTime startDate, DateTime endDate);
        Task<IEnumerable> BusiestTablePerDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable> BusiestHoursPerDay(DateTime startDate, DateTime endDate);
        Task<IEnumerable> MaxInvoiceBillPerDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable> MinInvoiceBillPerDate(DateTime startDate, DateTime endDate);
        Task<IEnumerable> AvgInvoiceBillPerDate(DateTime startDate, DateTime endDate);


        Task<int> TotalNumberOfCustomerVisits();
        Task<string> MostOrderedDish();
        Task<string> CostliestDish();
        Task<IEnumerable> CostliestDishByCategory();
        Task<string> CheapestDish();
        Task<IEnumerable> CheapestDishByCategory();
        Task<IEnumerable> InvoiceStatusReport();
        Task<IEnumerable> OrdersByCategory();
        Task<IEnumerable> OrdersByDishAndCategory();
        Task<IEnumerable> FeedbackByRatings();
    }
}
