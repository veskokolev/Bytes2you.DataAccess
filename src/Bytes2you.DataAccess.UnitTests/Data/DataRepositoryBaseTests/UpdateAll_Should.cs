using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class UpdateAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.UpdateAll(null);
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
                this.DataRepository.UpdateAll(entities);
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
                this.DataRepository.UpdateAll(entities);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterDirtyMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 1},
                new PersonDataEntityMock() { Id = 2 },
                new PersonDataEntityMock() { Id = 3 }
            };

            // Act.
            this.DataRepository.UpdateAll(entities);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterDirtyCalls(entities);
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
                    this.UnitOfWorkMock.AssertRegisterDirtyCalls(entities);
                    isCommitCalled = true;
                };

            this.DataRepository.UpdateAll(entities);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 1},
                new PersonDataEntityMock() { Id = 2 },
                new PersonDataEntityMock() { Id = 3 }
            };

            // Act.
            PersonDataEntityMock[] resultEntity = this.DataRepository.UpdateAll(entities);

            // Assert.
            CollectionAssert.AreEqual(entities, resultEntity);
        }
    }
}