using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Data.Repository
{
    public interface IRepository
    {
        IQueryable<T> FindAll<T>() where T : class;
        IQueryable<T> FindByCondition<T>(Expression<Func<T, bool>> expression) where T : class;
        Task<T> GetByID<T>(int id) where T : class;
        T Get<T>(int id) where T : class;
        Task Create<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void Save();
        Task SaveAsync();
        Task SaveBulk<T>(List<T> entity) where T : class;
    }
}
