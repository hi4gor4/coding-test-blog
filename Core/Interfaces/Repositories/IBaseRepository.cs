using System.Linq.Expressions;

namespace Core.Interfaces.Repositories;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetByIdAsync(long id);
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<T> AddAsync(T entity, CancellationToken cancellation);
    Task UpdateAsync(T entity, CancellationToken cancellation);
    Task DeleteAsync(T entity, CancellationToken cancellation);
    Task SaveChangesAsync(CancellationToken cancellation);
}