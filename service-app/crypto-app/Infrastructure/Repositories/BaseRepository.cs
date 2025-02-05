using System.Linq.Expressions;
using crypto_app.Core.Entities;
using crypto_app.Infrastructure.Data;
using crypto_app.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace crypto_app.Infrastructure.Repositories
{
public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CCDbContext _dbContext;

        protected DbSet<T> _dbSet;

        protected BaseRepository(CCDbContext context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual ICollection<T> GetAll(int limit = 30, int offset = 0, Expression<Func<T, bool>> search = null)
    {
            var query = _dbSet.AsQueryable();
            if (search != null)
            {
                query = query.Where(search);
            }

            return query.OrderByDescending(t => t.CreatedDateTime).Skip(offset).Take(limit).ToList();
        }

        public async Task<ICollection<T>> GetAllAsync(int limit = 30, int offset = 0, Expression<Func<T, bool>> search = null)
        {
            var query = _dbSet.AsQueryable();
            if (search != null)
            {
                query = query.Where(search);
            }

            var result = await query.OrderByDescending(t => t.CreatedDateTime).Skip(offset).Take(limit).ToListAsync();
            return result;
        }

        public T Get(Guid id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public Task<T> GetAsync(Guid id)
        {
            return _dbContext.Set<T>().FindAsync(id).AsTask();
        }

        public T Find(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefault(match);
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(match);
        }

        public ICollection<T> FindAll(Expression<Func<T, bool>> match)
        {
            return _dbContext.Set<T>().Where(match).ToList();
        }

        public virtual async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            var result = await _dbContext.Set<T>().Where(match).ToListAsync();
            return result;
        }

        public T Create(T t)
        {
            _dbContext.Set<T>().Add(t);
            _dbContext.SaveChanges();
            return t;
        }

        public ICollection<T> CreateRange(IEnumerable<T> t)
        {
            _dbContext.Set<T>().AddRange(t);
            _dbContext.SaveChanges();
            return t.ToList();
        }

        public async Task<T> CreateAsync(T t)
        {
            _dbContext.Set<T>().Add(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public T Update(Guid key, T updated)
        {
            if (updated == null)
            {
                return null;
            }

            T existing = _dbContext.Set<T>().Find(key);
            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(updated);
            }
            else
            {
                _dbContext.Set<T>().Add(updated);
            }
            _dbContext.SaveChanges();
            return existing;
        }

        public ICollection<T> UpdateRange(IEnumerable<T> t)
        {
            _dbContext.UpdateRange(t);
            _dbContext.SaveChanges();
            return t.ToList();
        }

        public async Task<ICollection<T>> UpdateRangeAsync(IEnumerable<T> t)
        {
            _dbContext.UpdateRange(t);
            await _dbContext.SaveChangesAsync();
            return t.ToList();
        }

        public async Task<T> UpdateAsync(Guid key, T updated)
        {
            if (updated == null)
            {
                return null;
            }

            T existing = _dbContext.Set<T>().Find(key);
            if (existing != null)
            {
                _dbContext.Entry(existing).CurrentValues.SetValues(updated);
            }
            else
            {
                _dbContext.Set<T>().Add(updated);
            }
            await _dbContext.SaveChangesAsync();
            return existing;
        }

        public void Delete(T t)
        {
            _dbContext.Set<T>().Remove(t);
            _dbContext.SaveChanges();
        }

        public Task DeleteAsync(T t)
        {
            _dbContext.Set<T>().Remove(t);
            return _dbContext.SaveChangesAsync();
        }

        public int Count()
        {
            return _dbContext.Set<T>().Count();
        }

        public Task<int> CountAsync()
        {
            return _dbContext.Set<T>().CountAsync();
        }

        public int Count(Expression<Func<T, bool>> search)
        {
            return _dbContext.Set<T>().Where(search).Count();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> search)
        {
            return _dbContext.Set<T>().Where(search).CountAsync();
        }

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}