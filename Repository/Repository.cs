using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal DbContext _dbContext;
        internal DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsQueryable().Where(predicate);
        }

        public async Task<T> GetSingle(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetSingle(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsQueryable().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> Post(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Put(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}