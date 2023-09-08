using System.Linq.Expressions;

namespace Wanderer.Domain.Repositories.Generics
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);

        Task Delete(T entity);

        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        
        Task<T> GetById(Guid id);

        Task Update(T entity);
    }
}