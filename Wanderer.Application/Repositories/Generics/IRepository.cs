﻿using System.Linq.Expressions;

namespace Wanderer.Infrastructure.Repositories.Generics;

public interface IRepository<T> where T : class
{
    Task InsertAsync(T entity);

    Task DeleteAsync(T entity);

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");

    Task<T> GetByIdAsync(Guid id);

    Task UpdateAsync(T entity);
}