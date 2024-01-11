using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HMS.Data.Repository
{
    public class Repository : IRepository
    {
        private readonly HmsContext _hmsContext;

        public Repository(HmsContext hmsContext)
        {
            _hmsContext = hmsContext;
        }

        //Todo : Create seprate methods async and normal for same function.

        public IQueryable<T> FindAll<T>() where T : class => _hmsContext.Set<T>().AsNoTracking();


        public IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class =>
            _hmsContext.Set<T>().Where(expression).AsNoTracking();


        public async Task<T> GetByID<T>(int id) where T : class => await _hmsContext.Set<T>().FindAsync(id);


        public T Get<T>(int id) where T : class => _hmsContext.Set<T>().Find(id);


        public async Task Create<T>(T entity) where T : class => await _hmsContext.Set<T>().AddAsync(entity);


        public void Update<T>(T entity) where T : class => _hmsContext.Set<T>().Update(entity);


        public async Task UpdateAsync<T>(T entity) where T : class
        {
            EntityEntry entityentry = _hmsContext.Entry(entity);
            entityentry.State = EntityState.Modified;
        }

        public void Delete<T>(T entity) where T : class => _hmsContext.Set<T>().Remove(entity);


        public async Task DeleteAsync<T>(T entity) where T : class
        {
            EntityEntry entityentry = _hmsContext.Entry(entity);
            entityentry.State = EntityState.Deleted;
        }

        public void Save() => _hmsContext.SaveChanges();


        public async Task SaveAsync() => await _hmsContext.SaveChangesAsync();

        public async Task SaveBulk<T>(List<T> entity) where T : class
        {
            await _hmsContext.AddRangeAsync(entity);
        }
    }
}