using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class Insert_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.Insert(null);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntityArgumentHasNonDefaultId()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock() { Id = 3 };

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.DataRepository.Insert(entity);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterNewMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            this.DataRepository.Insert(entity);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterNewCalls(entity);
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

            this.DataRepository.Insert(entity);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntityArgument_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock entity = new PersonDataEntityMock();

            // Act.
            PersonDataEntityMock resultEntity = this.DataRepository.Insert(entity);

            // Assert.
            Assert.AreSame(entity, resultEntity);
        }
    }
}