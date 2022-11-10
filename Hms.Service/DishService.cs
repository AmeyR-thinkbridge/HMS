using Hms.Models;
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
    public class DishService : IDishService
    {
        private readonly IRepository _repository;

        public DishService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dish> AddDishAsync(int dishCategoryId, DishViewModel dishViewModel)
        {
            var dish = new Dish()
            {
                Name = dishViewModel.Name,
                MRP = dishViewModel.MRP,
                DishCategroyId = dishCategoryId
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

        public async Task<bool> UpdateDishAsync(int id,int dishCategoryId, DishViewModel dishViewModel)
        {
            var dish = new Dish()
            {
                DishId = id,
                DishCategroyId = dishCategoryId,
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
    }

    public interface IDishService
    {
        Task<Dish> AddDishAsync(int dishCategoryId, DishViewModel dishViewModel);
        Task<List<Dish>> GetAllDishes();
        Task<Dish> GetDishbyID(int id);
        Task<bool> UpdateDishAsync(int id, int dishCategoryId, DishViewModel dishViewModel);
        bool Delete(Dish dish);
    }
}
