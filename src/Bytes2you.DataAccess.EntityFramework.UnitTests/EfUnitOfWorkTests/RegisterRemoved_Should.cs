using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
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
                this.EfUnitOfWork.RegisterRemoved(null);
            }, "entity");
        }

        [TestMethod]
        public void CallDbSetAttachMethodWithTheGivenEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.EfUnitOfWork.RegisterRemoved(entity);

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

            this.EfUnitOfWork.RegisterRemoved(entity);

            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAttachCalls(entity);
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertRemoveCalls(entity);
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
                    this.EfUnitOfWork.RegisterRemoved(entity);
                },
                ExecutionTimeType.Fast);
        }
    }
}