using Hms.Models;
using Hms.Models.ViewModels;
using Hms.Service.Helpers;
using HMS.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Hms.Service
{
    public class DishCategoryService : IDishCategoryService
    {
        private readonly IRepository _repository;

        public DishCategoryService(IRepository repository)
        {
            _repository = repository;
        }
        //Todo : Use DTO or viewmodel to return data as a response and accept data.
        //Todo : Use await _repository.GetByID<DishCategroy>(id); before updating we need to check if that entity exsists or not.
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
        public async Task<List<DishCategroy>> GetAllDishCategories()
        {
            var lstDishCategory = await _repository.FindAll<DishCategroy>().ToListAsync();
            return lstDishCategory;
        }

        public async Task<DishCategroy> GetDishCategorybyID(int id)
        {
            var dishCategroy = await _repository.GetByID<DishCategroy>(id);
            return dishCategroy;

        }

        public async Task<bool> UpdateDishCategoryAsync(int id, DishCategoryViewModel dishCategoryViewModel)
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

        public async Task<List<DishCategroy>> ConvertExcelToList(IFormFile file)
        {
            var list = new List<DishCategroy>();
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                    worksheet.TrimEmptyRows();
                    var rowcount = worksheet.Dimension.Rows;
                    int col = 1;
                    for (int row = 2; row <= rowcount; row++)
                    {
                        list.Add(new DishCategroy
                        {
                            CategoryCode = worksheet.Cells[row, col].Value.ToString().Trim(),
                            Description = worksheet.Cells[row, col + 1].Value.ToString().Trim()
                        });
                    }
                }
            }
            //var comparelist = await _repository.FindByCondition<DishCategroy>(async l=>l.CategoryCode==list.(a=>a.)).ToListAsync();
            return list;
        }

        public async Task<bool> SaveRange(List<DishCategroy> list)
        {
            if (list is not null && list.Count > 0)
            {
                await _repository.SaveBulk(list);
                await _repository.SaveAsync();
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
        Task<List<DishCategroy>> ConvertExcelToList(IFormFile file);
        Task<bool> SaveRange(List<DishCategroy> list);
    }
}
