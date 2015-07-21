using System;
using System.Collections.Generic;
using System.Linq;

namespace Bytes2you.DataAccess
{
    public interface IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        TEntity GetById(TId id);
        IList<TEntity> GetAll();
        int GetCount();

        TEntity Insert(TEntity entity);
        IList<TEntity> InsertAll(IEnumerable<TEntity> entities);

        TEntity Update(TEntity entity);
        IList<TEntity> UpdateAll(IEnumerable<TEntity> entities);
     
        TEntity Save(TEntity entity);
        IList<TEntity> SaveAll(IEnumerable<TEntity> entities);

        void Delete(TId id);
        void Delete(TEntity entity);
        void DeleteAll(IEnumerable<TEntity> entities);
    }
}