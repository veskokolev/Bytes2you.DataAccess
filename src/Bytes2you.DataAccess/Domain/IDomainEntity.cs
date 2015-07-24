using System;
using System.Linq;

namespace Bytes2you.DataAccess.Domain
{
    public interface IDomainEntity<TId> : IEntity<TId>
    {
    }
}