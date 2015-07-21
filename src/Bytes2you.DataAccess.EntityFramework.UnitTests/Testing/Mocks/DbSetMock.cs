using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks
{
    public class DbSetMock<TEntity> : DbSet<TEntity>, IEnumerable<TEntity>, IQueryable
        where TEntity : class
    {
        private readonly List<TEntity> internalSet;

        private readonly List<object> findCalls;
        private readonly List<TEntity> findReturnValues;
        private readonly List<TEntity> addCalls;
        private readonly List<TEntity> attachCalls;
        private readonly List<TEntity> removeCalls;
        private int asNoTrackingCallCount;

        public DbSetMock()
        {
            this.internalSet = new List<TEntity>();

            this.findCalls = new List<object>();
            this.findReturnValues = new List<TEntity>();
            this.addCalls = new List<TEntity>();
            this.attachCalls = new List<TEntity>();
            this.removeCalls = new List<TEntity>();
        }

        IQueryProvider IQueryable.Provider
        {
            get
            {
                return this.internalSet.AsQueryable().Provider;
            }
        }

        Expression IQueryable.Expression
        {
            get
            {
                return this.internalSet.AsQueryable().Expression;
            }
        }

        public override TEntity Add(TEntity entity)
        {
            this.internalSet.Add(entity);
            this.addCalls.Add(entity);

            return entity;
        }

        public override IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            this.internalSet.AddRange(entities);

            return this.internalSet;
        }

        public override TEntity Attach(TEntity entity)
        {
            this.OnAttaching();
            this.attachCalls.Add(entity);

            return entity;
        }

        public override TEntity Remove(TEntity entity)
        {
            this.internalSet.Remove(entity);
            this.removeCalls.Add(entity);

            return entity;
        }

        public override TEntity Find(params object[] keyValues)
        {
            TEntity result = (TEntity)Activator.CreateInstance(typeof(TEntity));

            this.findCalls.Add(keyValues.Single());
            this.findReturnValues.Add(result);

            return result;
        }

        public override DbQuery<TEntity> AsNoTracking()
        {
            this.asNoTrackingCallCount++;

            return this;
        }

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.internalSet.GetEnumerator();
        }

        public void AssertFindCalls(params object[] calls)
        {
            CollectionAssert.AreEqual(this.findCalls, calls);
        }

        public void AssertFindReturnValues(params TEntity[] returnValues)
        {
            CollectionAssert.AreEqual(this.findReturnValues, returnValues);
        }

        public void AssertAddCalls(params TEntity[] calls)
        {
            CollectionAssert.AreEqual(this.addCalls, calls);
        }
        
        public void AssertAttachCalls(params TEntity[] calls)
        {
            CollectionAssert.AreEqual(this.attachCalls, calls);
        }

        public void AssertRemoveCalls(params TEntity[] calls)
        {
            CollectionAssert.AreEqual(this.removeCalls, calls);
        }

        public void AssertAsNoTrackingCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.asNoTrackingCallCount);
        }

        public event EventHandler Attaching;
        protected virtual void OnAttaching()
        {
            if (this.Attaching != null)
            {
                this.Attaching(this, EventArgs.Empty);
            }
        }
    }
}