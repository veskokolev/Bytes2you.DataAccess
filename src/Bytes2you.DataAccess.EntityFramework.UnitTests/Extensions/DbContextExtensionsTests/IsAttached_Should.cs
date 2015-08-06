using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.Extensions;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Extensions.DbContextExtensionsTests
{
    [TestClass]
    public class IsAttached_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenDbContextArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                DbContextExtensions.IsAttached(null, new PersonDataEntityMock());
            }, "@dbContext");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                DbContextExtensions.IsAttached<int>(new DbContextMock(), null);
            }, "entity");
        }

        [TestMethod]
        public void ReturnTrue_WhenEntityStateIsUnchanged()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Unchanged;

            // Act.
            bool result = dbContext.IsAttached(entity);

            // Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnTrue_WhenEntityStateIsAdded()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Added;

            // Act.
            bool result = dbContext.IsAttached(entity);

            // Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnTrue_WhenEntityStateIsDleted()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Deleted;

            // Act.
            bool result = dbContext.IsAttached(entity);

            // Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnTrue_WhenEntityStateIsModified()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Modified;

            // Act.
            bool result = dbContext.IsAttached(entity);

            // Assert.
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ReturnFalse_WhenEntityStateIsDetached()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act.
            bool result = dbContext.IsAttached(entity);

            // Assert.
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    dbContext.IsAttached(entity);
                },
                ExecutionTimeType.Normal);
        }
    }
}