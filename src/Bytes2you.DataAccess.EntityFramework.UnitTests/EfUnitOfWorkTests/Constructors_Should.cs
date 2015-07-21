using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class Constructors_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenDbContextArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                new EfUnitOfWork(null);
            }, "dbContext");
        }

        [TestMethod]
        public void SetAllGivenProperties()
        {
            // Arrange.
            DbContextMock dbContextMock = new DbContextMock();

            // Act.
            EfUnitOfWork efUnitOfWork = new EfUnitOfWork(dbContextMock);

            // Assert.
            Assert.AreSame(dbContextMock, efUnitOfWork.DbContext);
        }
    }
}