using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Database.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly DatabaseContext _context;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DatabaseContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(long id, CancellationToken cancellation)
    {
        return await _dbSet.FindAsync(id, cancellation);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbSet.ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T> AddAsync(T entity, CancellationToken cancellation)
    {
        _context.Set<T>().Add(entity);
        await SaveChangesAsync(cancellation);
        return entity;
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellation)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync(cancellation);
    }

    public async Task DeleteAsync(T entity, CancellationToken cancellation)
    {
        entity.Deleted = true;
        await UpdateAsync(entity, cancellation);
    }

    public async Task SaveChangesAsync(CancellationToken cancellation)
    {
        await _context.SaveChangesAsync(cancellation);
    }
}
