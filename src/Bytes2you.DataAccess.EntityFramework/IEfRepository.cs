using System;
using System.Linq;

namespace Bytes2you.DataAccess.EntityFramework
{
    public interface IEfRepository<TEntity, TId> : IRepository<TEntity, TId>
        where TEntity : class, IEntity<TId>
    {
        IEfUnitOfWork UnitOfWork { get; }
    }
}