using System;
using System.Linq;

namespace Bytes2you.DataAccess
{
    public interface IEntity<TId>
    {
        TId Id { get; set; }
    }
}