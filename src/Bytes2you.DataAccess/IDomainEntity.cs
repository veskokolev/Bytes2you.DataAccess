using System;
using System.Linq;

namespace Bytes2you.DataAccess
{
    public interface IDomainEntity<TId> : IEntity<TId>
    {
    }
}