using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class RegisterRemoved_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EfUnitOfWork.RegisterRemoved<PersonDataEntityMock, int>(null);
            }, "entity");
        }

        [TestMethod]
        public void CallDbSetAttachMethodWithTheGivenEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.EfUnitOfWork.RegisterRemoved<PersonDataEntityMock, int>(entity);

            // Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAttachCalls(entity);
        }

        [TestMethod]
        public void CallDbSetRemoveMethodWithTheGivenEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act & Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().Attaching +=
                (s, a) =>
                {
                    this.DbContextMock.MockSet<PersonDataEntityMock>().AssertRemoveCalls();
                };

            this.EfUnitOfWork.RegisterRemoved<PersonDataEntityMock, int>(entity);

            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAttachCalls(entity);
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertRemoveCalls(entity);
        }
    }
}