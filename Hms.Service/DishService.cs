using Hms.Models;
using Hms.Models.ViewModels;
using HMS.Data.Repository;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Service
{
    public class DishService : IDishService
    {
        private readonly IRepository _repository;

        public DishService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dish> AddDishAsync(DishViewModel dishViewModel)
        {
            var dish = new Dish()
            {
                Name = dishViewModel.Name,
                MRP = dishViewModel.MRP,
                DishCategroyId = dishViewModel.DishCategroyId
            };
            await _repository.Create(dish);
            await _repository.SaveAsync();

            return dish;
        }

        public Task<List<Dish>> GetAllDishes()
        {
            var dishlist = _repository.FindAll<Dish>().ToListAsync();
            return dishlist;
        }

        public async Task<Dish> GetDishbyID(int id)
        {
            var dish = await _repository.GetByID<Dish>(id);
            return dish;
        }

        public async Task<double> GetDishMrpByID(int id)
        {
            var dishes = await _repository.GetByID<Dish>(id);
            return dishes.MRP;
        }

        public async Task<bool> UpdateDishAsync(int id, DishViewModel dishViewModel)
        {
            var dish = new Dish()
            {
                DishId = id,
                DishCategroyId = dishViewModel.DishCategroyId,
                Name = dishViewModel.Name,
                MRP = dishViewModel.MRP,
            };
            _repository.Update(dish);
            await _repository.SaveAsync();
            return true;
        }

        public bool Delete(Dish dish)
        {
            if (dish != null)
            {
                _repository.Delete(dish);
                _repository.Save();
                return true;
            }

            return false;
        }

        private async Task<List<Dish>> GetDishByConstraints(DishSearchViewModel dishSearchViewModel)
        {
            var dishlist = _repository.FindAll<Dish>();
            if (dishSearchViewModel.DishCategoryId != null)
            {
                dishlist = dishlist.Where(l => l.DishCategroyId == dishSearchViewModel.DishCategoryId);
            }
            if(dishSearchViewModel.Id != null)
            {
                dishlist = dishlist.Where(l=>l.DishId == dishSearchViewModel.Id);
            }
            if (!string.IsNullOrEmpty( dishSearchViewModel.Name))
            {
                dishlist = dishlist.Where(l => l.Name!.Contains(dishSearchViewModel.Name));
            }
            if (!dishSearchViewModel.OrderByAscending)
            {
                dishlist = dishlist.OrderByDescending(l => l.MRP);
            }
            else
            {
                dishlist = dishlist.OrderBy(l => l.MRP);
            }
            if(dishSearchViewModel.PageNumber != null && dishSearchViewModel.PageNumber > 1 && (!dishSearchViewModel.IsExcel.HasValue || !(bool)dishSearchViewModel.IsExcel) && dishSearchViewModel.PageNumber != null)
            {
                dishlist = dishlist.Skip(dishSearchViewModel.DishNumber!.Value * ((int)dishSearchViewModel.PageNumber - 1));
            }
            if (dishSearchViewModel.DishNumber != null && dishSearchViewModel.DishNumber > 0 && (!dishSearchViewModel.IsExcel.HasValue || !(bool)dishSearchViewModel.IsExcel))
            {
                dishlist = dishlist.Take(dishSearchViewModel.DishNumber!.Value);
            }


            return await dishlist.ToListAsync();
        }

        public async Task<dynamic> GetDishesByConstraints(DishSearchViewModel dishSearchViewModel)
        {
            dynamic dishlist;
            dishlist = await GetDishByConstraints(dishSearchViewModel);
            if (dishSearchViewModel.IsExcel == null || !(bool)dishSearchViewModel.IsExcel)
            {
                return dishlist;
            }

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(dishlist, true);
                package.Save();
            }
            stream.Position = 0;
            return stream;
        }
    }

    public interface IDishService
    {
        Task<Dish> AddDishAsync(DishViewModel dishViewModel);
        Task<List<Dish>> GetAllDishes();
        Task<Dish> GetDishbyID(int id);
        Task<bool> UpdateDishAsync(int id, DishViewModel dishViewModel);
        bool Delete(Dish dish);
        Task<double> GetDishMrpByID(int id);
        Task<dynamic> GetDishesByConstraints(DishSearchViewModel dishSearchViewModel);
    }
}
