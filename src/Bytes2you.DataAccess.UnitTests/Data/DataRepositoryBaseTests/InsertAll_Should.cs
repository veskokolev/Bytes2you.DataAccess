using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class InsertAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.InsertAll(null);
            }, "entities");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentContainsNull()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                null,
                new PersonDataEntityMock()
            };

            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DataRepository.InsertAll(entities);
            }, "entity");
        }

        [TestMethod]
        public void ThrowArgumentException_WhenEntitiesArgumentContainsEntityWhithNonDefaultId()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock()
            };

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.DataRepository.InsertAll(entities);
            }, "entity.Id");
        }

        [TestMethod]
        public void CallUnitOfWorkRegisterNewMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act.
            this.DataRepository.InsertAll(entities);

            // Assert.
            this.UnitOfWorkMock.AssertRegisterNewCalls(entities);
        }

        [TestMethod]
        public void CallUnitOfWorkCommitMethodForTheRegisteredEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };
            bool isCommitCalled = false;

            // Act & Assert.
            this.UnitOfWorkMock.Committing +=
                (s, a) =>
                {
                    // Assert.
                    this.UnitOfWorkMock.AssertRegisterNewCalls(entities);
                    isCommitCalled = true;
                };

            this.DataRepository.InsertAll(entities);

            Assert.IsTrue(isCommitCalled);
        }

        [TestMethod]
        public void ReturnTheSameEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act.
            PersonDataEntityMock[] resultEntity = this.DataRepository.InsertAll(entities);

            // Assert.
            CollectionAssert.AreEqual(entities, resultEntity);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock(),
                new PersonDataEntityMock(),
                new PersonDataEntityMock()
            };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.InsertAll(entities);
                },
                ExecutionTimeType.Normal);
        }
    }
}