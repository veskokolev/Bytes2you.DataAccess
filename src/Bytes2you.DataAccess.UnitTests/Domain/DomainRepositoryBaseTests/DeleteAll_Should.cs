using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class DeleteAll_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.DeleteAll(null);
            }, "entities");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentContainsNull()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock() { Id = 1 },
                    null,
                    new PersonDomainEntityMock() { Id = 1 }
                };

            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.DeleteAll(domainEntities);
            }, "entity");
        }

        [TestMethod]
        public void CallEntityMapperToDataEntityMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 }
                };

            // Act.
            this.DomainRepository.DeleteAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterRemovedCalls.ToArray();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(domainEntities, dataEntities));
        }

        [TestMethod]
        public void CallDataRepositoryDeleteAllMethod_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 }
                };

            // Act.
            this.DomainRepository.DeleteAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterRemovedCalls.ToArray();
            this.DataRepositoryMock.AssertDeleteAllCalls(dataEntities);
        }

        [TestMethod]
        public void CallEntityMappertCopyPropertiesToDomainEntityMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 }
                };

            // Act.
            this.DomainRepository.DeleteAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterRemovedCalls.ToArray();
            this.EntityMapperMock.AssertCopyPropertiesToDomainEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(dataEntities, domainEntities));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 },
                    new PersonDomainEntityMock() { Id = 1 }
                };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.DeleteAll(domainEntities);
                },
                ExecutionTimeType.Normal);
        }
    }
}