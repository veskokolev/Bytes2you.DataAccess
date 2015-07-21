using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.DataRepositoryBaseTests
{
    [TestClass]
    public class Delete_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentException_WhenIdArgumentIsDefault()
        {
            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.DataRepository.Delete(0);
            }, "id");
        }

        [TestMethod]
        public void CallDeleteMethodWithEntityHavingThePassedId_WhenIdArgumentIsValid()
        {
            // Arrange.
            int id = 3;

            // Act.
            this.DataRepository.Delete(id);

            // Assert.
            this.DataRepository.AssertDeleteEntityCalls(id);
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.Delete(null);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityArgumentHasDefaultId()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.DataRepository.Delete(entity);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterRemovedMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act.
            this.DataRepository.Delete(entity);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterRemovedCalls(entity);
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredRemovedEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterRemovedCalls(entity);
                    isCommitCalled = true;
                };

            this.DataRepository.Delete(entity);

            Assert.IsTrue(isCommitCalled);
        }
    }
}