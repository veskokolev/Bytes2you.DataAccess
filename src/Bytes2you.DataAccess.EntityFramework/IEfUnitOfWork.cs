using System;
using System.Data.Entity;
using System.Linq;

namespace Bytes2you.DataAccess.EntityFramework
{
    public interface IEfUnitOfWork : IUnitOfWork, IDisposable
    {
        DbContext DbContext { get; }
    }
}