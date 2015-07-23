using System;
using System.Linq;

namespace Bytes2you.DataAccess.EntityFramework
{
    public abstract class EfRepository<TDataEntity, TId> : DataRepositoryBase<TDataEntity, TId>, IEfRepository<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
    {   
        private bool isDisposed;

        public EfRepository(IEfUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        IEfUnitOfWork IEfRepository<TDataEntity, TId>.UnitOfWork
        {
            get
            {
                return (IEfUnitOfWork)this.UnitOfWork;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (isDisposing)
            {
                ((IEfUnitOfWork)this.UnitOfWork).Dispose();
            }

            this.isDisposed = true;
        }

        ~EfRepository()
        {
            this.Dispose(false);
        }
    }
}