using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class Dispose_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void DisopseTheDbContext()
        {
            // Act.
            this.EfUnitOfWork.Dispose();

            // Assert.
            this.DbContextMock.AssertDisposeCallCount(1);
        }

        [TestMethod]
        public void DisposeOnlyOnceWhenCalledMultipleTimes()
        {
            // Act.
            this.EfUnitOfWork.Dispose();
            this.EfUnitOfWork.Dispose();
            this.EfUnitOfWork.Dispose();

            // Assert.
            this.DbContextMock.AssertDisposeCallCount(1);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EfUnitOfWork.Dispose();
                },
                ExecutionTimeType.SuperFast);
        }
    }
}