using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks
{
    public class DbContextMock : DbContext
    {
        private Dictionary<Type, object> entityTypeToDbSet;
        private int saveChangesCallCount;
        private int disposeCallCount;

        static DbContextMock()
        {
            Database.SetInitializer<DbContextMock>(null);
        }

        public DbContextMock()
        {
        }

        public DbSet<PersonDataEntityMock> People { get; set; }

        private Dictionary<Type, object> EntityTypeToDbSet
        {
            get
            {
                if (this.entityTypeToDbSet == null)
                {
                    return this.entityTypeToDbSet = new Dictionary<Type, object>();
                }

                return this.entityTypeToDbSet;
            }
        }

        public override DbSet<TEntity> Set<TEntity>()
        {
            return this.MockSet<TEntity>();
        }

        public DbSetMock<TEntity> MockSet<TEntity>()
            where TEntity : class
        {
            if (!this.EntityTypeToDbSet.ContainsKey(typeof(TEntity)))
            {
                this.EntityTypeToDbSet.Add(typeof(TEntity), new DbSetMock<TEntity>());
            }

            return (DbSetMock<TEntity>)this.EntityTypeToDbSet[typeof(TEntity)];
        }

        public override int SaveChanges()
        {
            this.OnPreSaveChanges();

            this.saveChangesCallCount++;

            return 0;
        }

        protected override void Dispose(bool disposing)
        {
            this.disposeCallCount++;
        }

        public void AssertSaveCahngesCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.saveChangesCallCount);
        }

        public void AssertDisposeCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.disposeCallCount);
        }

        public event EventHandler PreSaveChanges;
        protected virtual void OnPreSaveChanges()
        {
            if (this.PreSaveChanges != null)
            {
                this.PreSaveChanges(this, EventArgs.Empty);
            }
        }
    }
}