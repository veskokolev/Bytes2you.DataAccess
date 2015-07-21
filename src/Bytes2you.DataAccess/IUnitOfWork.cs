using System;
using System.Collections.Generic;
using System.Linq;

namespace Bytes2you.DataAccess
{
    public interface IUnitOfWork
    {
        void RegisterNew<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>;

        void RegisterDirty<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>;

        void RegisterRemoved<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>;

        TDataEntity GetById<TDataEntity, TId>(TId id) where TDataEntity : class, IDataEntity<TId>;

        IList<TDataEntity> GetAll<TDataEntity, TId>() where TDataEntity : class, IDataEntity<TId>;

        int GetCount<TDataEntity, TId>() where TDataEntity : class, IDataEntity<TId>;

        void Commit();

        void Rollback();
    }
}