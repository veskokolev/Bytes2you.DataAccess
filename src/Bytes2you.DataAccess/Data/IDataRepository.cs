using System;
using System.Linq;

namespace Bytes2you.DataAccess.Data
{
    public interface IDataRepository<TDataEntity, TId> : IRepository<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
    {
    }
}