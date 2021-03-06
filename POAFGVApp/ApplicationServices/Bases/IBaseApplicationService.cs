﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POAFGVApp
{
    public interface IBaseApplicationService<T>
    {
        Task<int> InsertAsync(T TEntity);
        Task DeleteAsync(T TEntity);
        Task UpdateAsync(T TEntity);

        Task<T> GetByIdAsync(int pkId);
        Task<T> GetWithChildrenByPredicateAsync(Func<T, bool> predicate);
        Task<T> GetWithChildrenByPredicateAsync(int pk);

        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithChildrenByPredicateAsync(Expression<Func<T, bool>> predicate);
    }
}