using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace POAFGVApp
{
    public interface IBaseApplicationService<T> where T : BaseEntity
    {
        Task<int> InsertAsync(T TEntity);
        Task DeleteAsync(T TEntity);
        Task UpdateAsync(T TEntity);

        Task<T> GetByIdAsync(int pkId);
        Task<T> GetWithChildrenByPredicateAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithChildrenByPredicateAsync(Expression<Func<T, bool>> predicate);

        Task<T> GetRemoteData();

        Task<List<T>> GetAllRemoteData();
    }
}
