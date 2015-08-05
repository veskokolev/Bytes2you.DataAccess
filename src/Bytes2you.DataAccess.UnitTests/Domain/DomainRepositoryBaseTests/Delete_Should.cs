using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class Delete_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallDataRepositoryDeleteMethod_WhithTheGivenIdArgument()
        {
            // Arrange.
            int id = 5;

            // Act.
            this.DomainRepository.Delete(id);

            // Assert.
            this.DataRepositoryMock.AssertDeleteWithIdCalls(id);
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.Delete(null);
            }, "entity");
        }

        [TestMethod]
        public void CallEntityMapperToDataEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 3 };

            // Act.
            this.DomainRepository.Delete(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterRemovedCalls.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                new Tuple<PersonDomainEntityMock, PersonDataEntityMock>(domainEntity, dataEntity));
        }

        [TestMethod]
        public void CallDataRepositoryDeleteMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 3 };

            // Act.
            this.DomainRepository.Delete(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterRemovedCalls.Single();
            this.DataRepositoryMock.AssertDeleteWithEntityCalls(dataEntity.Id);
        }

        [TestMethod]
        public void CallEntityMappertCopyPropertiesToDomainEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 3 };

            // Act.
            this.DomainRepository.Delete(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterRemovedCalls.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDomainEntityOverrideCalls(
                new Tuple<PersonDataEntityMock, PersonDomainEntityMock>(dataEntity, domainEntity));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 3 };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.Delete(domainEntity);
                },
                ExecutionTimeType.Normal);
        }
    }
}