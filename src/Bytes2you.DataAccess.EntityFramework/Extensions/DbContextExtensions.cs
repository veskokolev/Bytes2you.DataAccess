using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.EntityFramework.Extensions
{
    public static class DbContextExtensions
    {
        public static bool IsAttached<TId>(this DbContext @dbContext, IDataEntity<TId> entity)
        {
            Guard.WhenArgument(@dbContext, "@dbContext").IsNull().Throw();
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            return @dbContext.Entry(entity).State != EntityState.Detached;
        }

        public static void ThrowIfAttached<TId>(this DbContext @dbContext, IDataEntity<TId> entity)
        {
            Guard.WhenArgument(@dbContext, "@dbContext").IsNull().Throw();
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            Guard.WhenArgument(dbContext.IsAttached(entity), "entity").IsTrue().Throw();
        }

        public static void DetachAll(this DbContext @dbContext)
        {
            Guard.WhenArgument(@dbContext, "@dbContext").IsNull().Throw();

            foreach (DbEntityEntry entry in @dbContext.ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }
    }
}