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
    public class DishCategoryService : IDishCategoryService
    {
        private readonly IRepository _repository;

        public DishCategoryService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<DishCategroy> AddDishCategoryAsync(DishCategoryViewModel dishCategoryViewModel)
        {
            var dishCategory = new DishCategroy()
            {
               CategoryCode = dishCategoryViewModel.CategoryCode,
               Description = dishCategoryViewModel.Description,
            };
            await _repository.Create(dishCategory);
            await _repository.SaveAsync();

            return dishCategory;
        }
        public Task<List<DishCategroy>> GetAllDishCategories()
        {
            var lstDishCategory = _repository.FindAll<DishCategroy>().ToListAsync();
            return lstDishCategory;
        }

        public async Task<DishCategroy> GetDishCategorybyID(int id)
        {
            var dishCategroy = await _repository.GetByID<DishCategroy>(id);
            return dishCategroy;

        }

        public async Task<bool> UpdateDishCategoryAsync(int id,DishCategoryViewModel dishCategoryViewModel)
        {
            var table = new DishCategroy()
            {
                CategoryId = id,
                CategoryCode = dishCategoryViewModel.CategoryCode,
                Description = dishCategoryViewModel.Description,
            };
            _repository.Update(table);
            await _repository.SaveAsync();
            return true;
        }

        public bool Delete(DishCategroy dishCategroy)
        {
            if (dishCategroy != null)
            {
                _repository.Delete(dishCategroy);
                _repository.Save();
                return true;
            }

            return false;
        }
    }

    public interface IDishCategoryService
    {
        Task<DishCategroy> AddDishCategoryAsync(DishCategoryViewModel dishCategoryViewModel);
        Task<List<DishCategroy>> GetAllDishCategories();
        Task<DishCategroy> GetDishCategorybyID(int id);
        Task<bool> UpdateDishCategoryAsync(int id, DishCategoryViewModel dishCategoryViewModel);
        bool Delete(DishCategroy dishCategroy);
    }
}
