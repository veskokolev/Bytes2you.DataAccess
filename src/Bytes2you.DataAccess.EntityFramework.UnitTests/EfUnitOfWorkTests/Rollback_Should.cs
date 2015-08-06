using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class Rollback_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void DetachTheTrackedEntitiesWithoutSavingThem()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 1 };
            this.DbContextMock.MockSet<PersonDataEntityMock>().Add(entity);
            this.DbContextMock.Entry(entity).State = EntityState.Added;

            // Act.
            this.EfUnitOfWork.Rollback();

            // Assert.
            this.DbContextMock.AssertSaveCahngesCallCount(0);
            Assert.AreEqual(EntityState.Detached, this.DbContextMock.Entry(entity).State);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 1 };
            this.DbContextMock.MockSet<PersonDataEntityMock>().Add(entity);
            this.DbContextMock.Entry(entity).State = EntityState.Added;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                     this.EfUnitOfWork.Rollback();
                },
                ExecutionTimeType.Normal);
        }
    }
}