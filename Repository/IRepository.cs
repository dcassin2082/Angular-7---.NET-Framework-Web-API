using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        Task<T> GetSingle(int id);
        Task<T> GetSingle(Expression<Func<T, bool>> predicate);
        Task<T> Post(T entity);
        Task<T> Put(T entity);
        Task<T> Delete(T entity);
    }
}