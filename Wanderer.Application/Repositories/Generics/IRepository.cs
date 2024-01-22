using System.Linq.Expressions;

namespace Wanderer.Infrastructure.Repositories.Generics;

public interface IRepository<T> where T : class
{
    Task Add(T entity);

    Task Delete(T entity);

    Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

    Task<T> GetById(Guid id);

    Task Update(T entity);
}