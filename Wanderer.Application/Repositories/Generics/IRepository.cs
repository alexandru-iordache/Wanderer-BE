using System.Linq.Expressions;
using Wanderer.Domain.Models;

namespace Wanderer.Infrastructure.Repositories.Generics;

public interface IRepository<T> where T : BaseEntity
{
    Task InsertAsync(T entity);

    Task InsertRangeAsync(IEnumerable<T> entities);

    void Delete(T entity);

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "");

    Task<T?> GetByIdAsync(Guid id, Expression<Func<T, bool>>? filter = null, string includeProperties = "");

    void Update(T entity);

    Task SaveChangesAsync();

    Task<IEnumerable<T>> GetBatchAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "", int skip = 0, int top = 25);
    
    Task<int> GetCountAsync(Expression<Func<T, bool>>? filter = null);
}