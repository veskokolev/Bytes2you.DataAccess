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
    public class DetachAll_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenDbContextArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                DbContextExtensions.DetachAll(null);
            }, "@dbContext");
        }

        [TestMethod]
        public void DetachAllTrackedEntities_WhenDbContextArgumentIsValid()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock[] entities =
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock() { Id = 1},
                    new PersonDataEntityMock() { Id = 2},
                    new PersonDataEntityMock() { Id = 3},
                    new PersonDataEntityMock() { Id = 4},
                    new PersonDataEntityMock() { Id = 5},
                };

            dbContext.Entry(entities[0]).State = EntityState.Detached;
            dbContext.Entry(entities[1]).State = EntityState.Unchanged;
            dbContext.Entry(entities[2]).State = EntityState.Added;
            dbContext.Entry(entities[3]).State = EntityState.Deleted;
            dbContext.Entry(entities[4]).State = EntityState.Modified;

            // Act.
            dbContext.DetachAll();

            // Assert.
            foreach (PersonDataEntityMock entity in entities)
            {
                Assert.AreEqual(EntityState.Detached, dbContext.Entry(entity).State);
            }
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            DbContextMock dbContext = new DbContextMock();
            PersonDataEntityMock[] entities =
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock() { Id = 1},
                    new PersonDataEntityMock() { Id = 2},
                    new PersonDataEntityMock() { Id = 3},
                    new PersonDataEntityMock() { Id = 4},
                    new PersonDataEntityMock() { Id = 5},
                };

            dbContext.Entry(entities[0]).State = EntityState.Detached;
            dbContext.Entry(entities[1]).State = EntityState.Unchanged;
            dbContext.Entry(entities[2]).State = EntityState.Added;
            dbContext.Entry(entities[3]).State = EntityState.Deleted;
            dbContext.Entry(entities[4]).State = EntityState.Modified;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    dbContext.DetachAll();
                },
                ExecutionTimeType.Normal);
        }
    }
}