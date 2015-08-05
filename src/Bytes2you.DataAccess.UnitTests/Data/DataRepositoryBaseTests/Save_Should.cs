using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class Save_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.Save(null);
            }, "entity");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterNewMethod_WhenEntityHasDefaultId()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.DataRepository.Save(entity);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterNewCalls(entity);
            this.UnitOfWorkMock.AssertRegisterDirtyCalls();
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterDirtyMethod_WhenEntityHasNonDefaultId()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act.
            this.DataRepository.Save(entity);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterDirtyCalls(entity);
            this.UnitOfWorkMock.AssertRegisterNewCalls();
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredNewEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterNewCalls(entity);
                    isCommitCalled = true;
                };

            this.DataRepository.Save(entity);

            Assert.IsTrue(isCommitCalled);
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

            this.DataRepository.Save(entity);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntityArgument_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act.
            PersonDataEntityMock resultEntity = this.DataRepository.Save(entity);

            // Assert.
            Assert.AreSame(entity, resultEntity);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.Save(entity);
                },
                ExecutionTimeType.Fast);
        }
    }
}