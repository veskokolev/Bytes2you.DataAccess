using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class SaveAll_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.SaveAll(null);
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
                this.DomainRepository.SaveAll(domainEntities);
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
            this.DomainRepository.SaveAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterNewCalls.ToArray();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(domainEntities, dataEntities));
        }

        [TestMethod]
        public void CallDataRepositorySaveAllMethod_WhenEntitiesArgumentIsValid()
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
            this.DomainRepository.SaveAll(domainEntities);

            // Assert.
            PersonDataEntityMock[] dataEntities = this.UnitOfWorkMock.RegisterNewCalls.ToArray();
            this.DataRepositoryMock.AssertSaveAllCalls(dataEntities);
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
            this.DomainRepository.SaveAll(domainEntities);

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
            PersonDomainEntityMock[] result = this.DomainRepository.SaveAll(domainEntities);

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

                    this.DomainRepository.SaveAll(domainEntities);
                },
                ExecutionTimeType.Normal);
        }
    }
}