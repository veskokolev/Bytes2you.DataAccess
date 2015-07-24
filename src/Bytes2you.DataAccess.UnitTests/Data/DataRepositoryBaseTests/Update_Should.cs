using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class Update_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.Update(null);
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
                this.DataRepository.Update(entity);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterDirtyMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act.
            this.DataRepository.Update(entity);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterDirtyCalls(entity);
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredDirtyEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterDirtyCalls(entity);
                    isCommitCalled = true;
                };

            this.DataRepository.Update(entity);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntityArgument_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act.
            PersonDataEntityMock resultEntity = this.DataRepository.Update(entity);

            // Assert.
            Assert.AreSame(entity, resultEntity);
        }
    }
}