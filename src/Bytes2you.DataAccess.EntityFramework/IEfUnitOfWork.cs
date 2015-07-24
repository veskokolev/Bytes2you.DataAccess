using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.EntityFramework
{
    public interface IEfUnitOfWork<TDataEntity, TId> : IUnitOfWork<TDataEntity, TId>, IDisposable
        where TDataEntity : class, IDataEntity<TId>
    {
        DbContext DbContext { get; }
    }
}