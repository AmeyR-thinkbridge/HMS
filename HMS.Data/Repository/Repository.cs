using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly HmsContext _hmsContext;

        public Repository(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }

        public IQueryable<T> FindAll<T>() where T : class => _hmsContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class =>
            _hmsContext.Set<T>().Where(expression).AsNoTracking();
        public async Task<T> GetByID<T>(int id) where T : class => await _hmsContext.Set<T>().FindAsync(id);
        public async Task Create<T>(T entity) where T : class => await _hmsContext.Set<T>().AddAsync(entity);
        public void Update<T>(T entity) where T : class => _hmsContext.Set<T>().Update(entity);
        public void Delete<T>(T entity) where T : class => _hmsContext.Set<T>().Remove(entity);
        public void Save() => _hmsContext.SaveChanges();
        public async Task SaveAsync() => await _hmsContext.SaveChangesAsync();
    }
}