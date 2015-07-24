using System;
using System.Linq;

namespace Bytes2you.DataAccess.Data
{
    public interface IUnitOfWork<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>
    {
        void RegisterNew(TDataEntity entity);
        void RegisterDirty(TDataEntity entity);
        void RegisterRemoved(TDataEntity entity);

        TDataEntity GetById(TId id);
        TDataEntity[] GetAll();
        int GetCount();

        void Commit();
        void Rollback();
    }
}