using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfRepositoryTests
{
    [TestClass]
    public class Dispose_Should : EfRepositoryTestBase
    {
        [TestMethod]
        public void DisopseTheDbContext()
        {
            // Act.
            this.EfRepositoryMock.Dispose();

            // Assert.
            this.EfUnitOfWork.AssertDisposeCallCount(1);
        }

        [TestMethod]
        public void DisposeOnlyOnceWhenCalledMultipleTimes()
        {
            // Act.
            this.EfRepositoryMock.Dispose();
            this.EfRepositoryMock.Dispose();
            this.EfRepositoryMock.Dispose();

            // Assert.
            this.EfUnitOfWork.AssertDisposeCallCount(1);
        }
    }
}