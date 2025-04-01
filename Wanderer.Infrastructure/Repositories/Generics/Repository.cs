using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Wanderer.Domain.Models;
using Wanderer.Infrastructure.Context;

namespace Wanderer.Infrastructure.Repositories.Generics;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly WandererDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected Repository(WandererDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    public virtual async Task<IEnumerable<T>> GetAsync(
        Expression<Func<T, bool>>? filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
        string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.ToListAsync();
    }

    public virtual async Task InsertAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
