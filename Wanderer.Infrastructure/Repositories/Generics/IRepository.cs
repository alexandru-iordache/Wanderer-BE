using System.Linq.Expressions;

namespace Wanderer.Infrastructure.Repositories.Generics
{
    public interface IRepository<T> where T : class
    {
        Task Add(T entity);
        Task Delete(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Guid id);
        Task Update(T entity);
    }
}