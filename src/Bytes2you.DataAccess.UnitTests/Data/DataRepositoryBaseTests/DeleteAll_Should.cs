using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class DeleteAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.DeleteAll(null);
            }, "entities");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentContainsNull()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                null,
                new PersonDataEntityMock() { Id = 4 }
            };

            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.DeleteAll(entities);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntitiesArgumentContainsEntityWhithDefaultId()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3},
                new PersonDataEntityMock(),
                new PersonDataEntityMock() { Id = 3 }
            };

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.DataRepository.DeleteAll(entities);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterRemovedMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 1},
                new PersonDataEntityMock() { Id = 2 },
                new PersonDataEntityMock() { Id = 3 }
            };

            // Act.
            this.DataRepository.DeleteAll(entities);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterRemovedCalls(entities);
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 1},
                new PersonDataEntityMock() { Id = 2 },
                new PersonDataEntityMock() { Id = 3 }
            };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterRemovedCalls(entities);
                    isCommitCalled = true;
                };

            this.DataRepository.DeleteAll(entities);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 1},
                new PersonDataEntityMock() { Id = 2 },
                new PersonDataEntityMock() { Id = 3 }
            };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.DeleteAll(entities);
                },
                ExecutionTimeType.Normal);
        }
    }
}