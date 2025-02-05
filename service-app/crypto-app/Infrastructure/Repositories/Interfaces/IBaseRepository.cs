using System.Linq.Expressions;
using crypto_app.Core.Entities;

namespace crypto_app.Infrastructure.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        ICollection<T> GetAll(int limit = 30, int offset = 0, Expression<Func<T, bool>> search = null);
        Task<ICollection<T>> GetAllAsync(int limit = 30, int offset = 0, Expression<Func<T, bool>> search = null);
        T Get(Guid id);
        Task<T> GetAsync(Guid id);
        T Find(Expression<Func<T, bool>> match);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
        ICollection<T> FindAll(Expression<Func<T, bool>> match);
        Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match);
        T Create(T t);
        ICollection<T> CreateRange(IEnumerable<T> t);
        Task<T> CreateAsync(T t);
        T Update(Guid key, T updated);
        ICollection<T> UpdateRange(IEnumerable<T> t);
        Task<ICollection<T>> UpdateRangeAsync(IEnumerable<T> t);
        Task<T> UpdateAsync(Guid key, T updated);
        void Delete(T t);
        Task DeleteAsync(T t);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<T, bool>> search);
        Task<int> CountAsync(Expression<Func<T, bool>> search);
        void SaveChanges();
        Task SaveChangesAsync();
    }
}