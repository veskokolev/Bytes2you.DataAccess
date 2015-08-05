using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class Commit_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void CallDbContextSaveChangesMethod()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 1 };
            this.DbContextMock.MockSet<PersonDataEntityMock>().Add(entity);
            this.DbContextMock.Entry(entity).State = EntityState.Added;

            // Act.
            this.EfUnitOfWork.Commit();

            // Assert.
            this.DbContextMock.AssertSaveCahngesCallCount(1);
            Assert.AreEqual(EntityState.Detached, this.DbContextMock.Entry(entity).State);
        }

        [TestMethod]
        public void DetachTheTrackedEntitiesAfterSavingTheChanges()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 1 };
            this.DbContextMock.MockSet<PersonDataEntityMock>().Add(entity);
            this.DbContextMock.Entry(entity).State = EntityState.Added;

            // Act & Assert.
            this.DbContextMock.PreSaveChanges +=
                (s, a) =>
                {
                    Assert.AreNotEqual(EntityState.Detached, this.DbContextMock.Entry(entity).State);
                };

            this.EfUnitOfWork.Commit();

            this.DbContextMock.AssertSaveCahngesCallCount(1);
            Assert.AreEqual(EntityState.Detached, this.DbContextMock.Entry(entity).State);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EfUnitOfWork.Commit();
                },
                4, 1000);
        }
    }
}