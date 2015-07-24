using System;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.EntityFramework
{
    public interface IEfRepository<TDataEntity, TId> : IDataRepository<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
    {
        IEfUnitOfWork<TDataEntity, TId> UnitOfWork { get; }
    }
}