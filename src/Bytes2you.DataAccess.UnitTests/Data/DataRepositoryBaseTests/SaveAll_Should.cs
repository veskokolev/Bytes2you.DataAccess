using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class SaveAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.SaveAll(null);
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
                new PersonDataEntityMock()
            };

            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.SaveAll(entities);
            }, "entity");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterNewMethodForEachNewEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act.
            this.DataRepository.SaveAll(entities);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterNewCalls(entities.Where(e => e.Id == 0).ToArray());
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterDirtyMethodForEachExistingEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act.
            this.DataRepository.SaveAll(entities);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterDirtyCalls(entities.Where(e => e.Id != 0).ToArray());
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredNewEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterNewCalls(entities.Where(e => e.Id == 0).ToArray());
                    isCommitCalled = true;
                };

            this.DataRepository.SaveAll(entities);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredDirtyEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterDirtyCalls(entities.Where(e => e.Id != 0).ToArray());
                    isCommitCalled = true;
                };

            this.DataRepository.SaveAll(entities);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act.
            PersonDataEntityMock[] resultEntity = this.DataRepository.SaveAll(entities);

            // Assert.
            CollectionAssert.AreEqual(entities, resultEntity);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.SaveAll(entities);
                },
                ExecutionTimeType.Normal);
        }
    }
}