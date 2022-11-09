using AutoMapper;
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
    public class TableService : ITableService
    {
        private readonly IRepository _repository;
        private readonly IMapper _mapper;

        public TableService(IRepository repository,
                            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Table> AddTableAsync(TableViewModel tableViewModel)
        {
            var table = new Table()
            {
                TableNumber = tableViewModel.TableNumber,
                Description = tableViewModel.Description,
            };
            await _repository.Create(table);
            await _repository.SaveAsync();

            return table;
        }

        public Task<List<Table>> GetAllTables()
        {
            var listoftables = _repository.FindAll<Table>().ToListAsync();
            return listoftables;
        }

        public async Task<Table> GetTablebyID(int id)
        {
            var table = await _repository.GetByID<Table>(id);
            return table;

        }

        public async Task<bool> UpdateTableAsync(int id,TableViewModel tableViewModel)
        {
            var table = new Table()
            {
                TableId = id,
                TableNumber = tableViewModel.TableNumber,
                Description = tableViewModel.Description,
            };
            _repository.Update(table);
            await _repository.SaveAsync();
            return true;
        }

        public bool Delete(Table table)
        {
            if (table != null)
            {
                _repository.Delete(table);
                _repository.Save();
                return true;
            }
            
            return false;
        }
    }

    public interface ITableService
    {
        Task<Table> AddTableAsync(TableViewModel tableViewModel);
        Task<List<Table>> GetAllTables();
        Task<Table> GetTablebyID(int id);
        Task<bool> UpdateTableAsync(int id, TableViewModel tableViewModel);
        bool Delete(Table table);
    }
}
