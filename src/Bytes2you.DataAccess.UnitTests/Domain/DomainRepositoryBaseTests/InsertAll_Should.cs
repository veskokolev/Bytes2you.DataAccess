using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class InsertAll_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.InsertAll(null);
            }, "entities");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentContainsNull()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock(),
                    null,
                    new PersonDomainEntityMock()
                };

            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.InsertAll(domainEntities);
            }, "entity");
        }

        [TestMethod]
        public void CallEntityMapperToDataEntityMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act.
            this.DomainRepository.InsertAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterNewCalls.ToArray();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(domainEntities, dataEntities));
        }

        [TestMethod]
        public void CallDataRepositoryInsertAllMethod_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act.
            this.DomainRepository.InsertAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterNewCalls.ToArray();
            this.DataRepositoryMock.AssertInsertAllCalls(dataEntities);
        }

        [TestMethod]
        public void CallEntityMappertCopyPropertiesToDomainEntityMethodForEachEntity_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act.
            this.DomainRepository.InsertAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterNewCalls.ToArray();
            this.EntityMapperMock.AssertCopyPropertiesToDomainEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(dataEntities, domainEntities));
        }

        [TestMethod]
        public void ReturnTheSameDomainEntities_WhenEntitiesArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities =
                new PersonDomainEntityMock[]
                {
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act.
            PersonDomainEntityMock[] result = this.DomainRepository.InsertAll(domainEntities);

            // Assert.
            CollectionAssert.AreEqual(domainEntities, result);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    PersonDomainEntityMock[] domainEntities =
                        new PersonDomainEntityMock[]
                        {
                            new PersonDomainEntityMock(),
                            new PersonDomainEntityMock(),
                            new PersonDomainEntityMock()
                        };

                    this.DomainRepository.InsertAll(domainEntities);
                },
                ExecutionTimeType.Normal);
        }
    }
}