using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks
{
    public class EfUnitOfWorkMock<TDataEntity, TId> : EfUnitOfWork<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>
    {
        private int disposeCallCount;

        public EfUnitOfWorkMock(DbContext dbContext)
            : base(dbContext)
        {
        }

        protected override void Dispose(bool isDisposing)
        {
            this.disposeCallCount++;

            base.Dispose(isDisposing);
        }

        public void AssertDisposeCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.disposeCallCount);
        }
    }
}