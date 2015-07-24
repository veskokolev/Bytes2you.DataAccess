using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    public abstract class EfUnitOfWorkTestBase
    {
        protected EfUnitOfWork<PersonDataEntityMock, int> EfUnitOfWork { get; private set; }
        protected DbContextMock DbContextMock { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange.
            this.DbContextMock = new DbContextMock();
            this.EfUnitOfWork = new EfUnitOfWork<PersonDataEntityMock, int>(this.DbContextMock);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.DbContextMock = null;
            this.EfUnitOfWork = null;
        }
    }
}