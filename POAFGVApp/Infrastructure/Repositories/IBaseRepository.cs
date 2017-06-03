using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace POAFGVApp
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        void Insert(T TEntity);
        void Delete(T TEntity);
        void Update(T TEntity);

        T GetById(int pkId);
        T GetWithChildrenByPredicate(Expression<Func<T, bool>> predicate);

        List<T> GetAll();
        List<T> GetAllWithChildrenByPredicate(Expression<Func<T, bool>> predicate);
    }
}