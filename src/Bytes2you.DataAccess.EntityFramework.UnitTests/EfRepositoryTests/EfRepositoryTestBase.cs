using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfRepositoryTests
{
    public abstract class EfRepositoryTestBase
    {
        protected EfRepositoryMock<PersonDataEntityMock, int> EfRepositoryMock { get; private set; }
        protected EfUnitOfWorkMock<PersonDataEntityMock, int> EfUnitOfWork { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange.
            DbContextMock dbContextMock = new DbContextMock();
            this.EfUnitOfWork = new EfUnitOfWorkMock<PersonDataEntityMock, int>(dbContextMock);
            this.EfRepositoryMock = new EfRepositoryMock<PersonDataEntityMock,int>(this.EfUnitOfWork);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.EfRepositoryMock = null;
            this.EfUnitOfWork = null;
        }
    }
}