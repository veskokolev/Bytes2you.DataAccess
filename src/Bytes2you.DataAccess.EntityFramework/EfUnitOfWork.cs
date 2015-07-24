using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.DataAccess.EntityFramework.Extensions;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.EntityFramework
{
    public class EfUnitOfWork<TDataEntity, TId> : IEfUnitOfWork<TDataEntity, TId>, IDisposable
        where TDataEntity : class, IDataEntity<TId>
    {
        private bool isDisposed;
        private readonly DbContext dbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public DbContext DbContext
        {
            get
            {
                return this.dbContext;
            }
        }

        public TDataEntity GetById(TId id)
        {
            return this.DbContext.Set<TDataEntity>().Find(id);
        }

        public TDataEntity[] GetAll()
        {
            return this.DbContext.Set<TDataEntity>().AsNoTracking().ToArray();
        }
        
        public int GetCount()
        {
            return this.DbContext.Set<TDataEntity>().Count();
        }

        public void RegisterNew(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            this.DbContext.ThrowIfAttached(entity);

            this.DbContext.Set<TDataEntity>().Add(entity);
        }

        public void RegisterDirty(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            this.DbContext.ThrowIfAttached(entity);

            this.DbContext.Set<TDataEntity>().Attach(entity);
			this.DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void RegisterRemoved(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            this.DbContext.Set<TDataEntity>().Attach(entity);
            this.DbContext.Set<TDataEntity>().Remove(entity);
        }

        public void Commit()
        {
            try
            {
                this.DbContext.SaveChanges();
            }
            finally
            {
                this.DbContext.DetachAll();
            }
        }

        public void Rollback()
        {
            this.DbContext.DetachAll();
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
                this.DbContext.Dispose();
            }

            this.isDisposed = true;
        }

        ~EfUnitOfWork()
        {
            this.Dispose(false);
        }
    }
}