using System;
using System.Linq;

namespace Bytes2you.DataAccess.Domain
{
    public interface IDomainRepository<TDomainEntity, TId> : IRepository<TDomainEntity, TId>
        where TDomainEntity : class, IDomainEntity<TId>, new()
    {
    }
}