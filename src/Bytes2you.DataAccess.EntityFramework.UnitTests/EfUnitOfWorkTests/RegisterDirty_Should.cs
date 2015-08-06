using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class RegisterDirty_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EfUnitOfWork.RegisterDirty(null);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityArgumentIsAttached()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();
            this.DbContextMock.Entry(entity).State = EntityState.Added;


            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.EfUnitOfWork.RegisterDirty(entity);
            }, "entity");
        }

        [TestMethod]
        public void CallDbSetAttachMethodWithTheGivenEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.EfUnitOfWork.RegisterDirty(entity);

            // Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAttachCalls(entity);
        }

        [TestMethod]
        public void SetsTheStateOfTheGivenEntityToModified_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act & Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().Attaching +=
                (s, a) =>
                {
                    Assert.AreEqual(EntityState.Detached, this.DbContextMock.Entry(entity).State);
                };

            this.EfUnitOfWork.RegisterDirty(entity);

            Assert.AreEqual(EntityState.Modified, this.DbContextMock.Entry(entity).State);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            int i = 0;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    PersonDataEntityMock entity = new PersonDataEntityMock() { Id = i++ };
                    this.EfUnitOfWork.RegisterDirty(entity);
                },
                5, 300);
        }
    }
}