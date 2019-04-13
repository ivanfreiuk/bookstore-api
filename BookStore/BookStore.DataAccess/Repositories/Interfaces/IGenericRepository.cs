using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BookStore.DataAccess.Repositories.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity: class
    {
        TEntity Get(int id);
        ICollection<TEntity> GetAll();
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(ICollection<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(ICollection<TEntity> entities);

        void Update(TEntity entity);

    }
}
