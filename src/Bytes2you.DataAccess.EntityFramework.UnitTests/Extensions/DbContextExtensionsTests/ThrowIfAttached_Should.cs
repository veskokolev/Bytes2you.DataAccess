using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.Extensions;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Extensions.DbContextExtensionsTests
{
    [TestClass]
    public class ThrowIfAttached_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenDbContextArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                DbContextExtensions.ThrowIfAttached(null, new PersonDataEntityMock());
            }, "@dbContext");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                DbContextExtensions.ThrowIfAttached<int>(new DbContextMock(), null);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityStateIsUnchanged()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Unchanged;

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                dbContext.ThrowIfAttached(entity);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityStateIsAdded()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Added;

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                dbContext.ThrowIfAttached(entity);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityStateIsDleted()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Deleted;

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                dbContext.ThrowIfAttached(entity);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityStateIsModified()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Modified;

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                dbContext.ThrowIfAttached(entity);
            }, "entity");
        }

        [TestMethod]
        public void DoNotThrowException_WhenEntityStateIsDetached()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock entity = new PersonDataEntityMock();
            dbContext.Entry(entity).State = EntityState.Detached;

            // Act & Assert.
            Ensure.NoExceptionIsThrown(() =>
            {
                dbContext.ThrowIfAttached(entity);
            });
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
                    dbContext.ThrowIfAttached(entity);
                },
                ExecutionTimeType.Normal);
        }
    }
}