using System;
using System.Data.Entity;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class RegisterNew_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EfUnitOfWork.RegisterNew<PersonDataEntityMock, int>(null);
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
                this.EfUnitOfWork.RegisterNew<PersonDataEntityMock, int>(entity);
            }, "entity");
        }

        [TestMethod]
        public void CallDbSetAddMethodWithTheGivenEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.EfUnitOfWork.RegisterNew<PersonDataEntityMock, int>(entity);

            // Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAddCalls(entity);
        }
    }
}