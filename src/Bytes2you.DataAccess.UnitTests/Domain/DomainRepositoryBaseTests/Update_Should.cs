using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class Update_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.Update(null);
            }, "entity");
        }

        [TestMethod]
        public void CallEntityMapperToDataEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 5 };

            // Act.
            this.DomainRepository.Update(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterDirtyCalls.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                new Tuple<PersonDomainEntityMock, PersonDataEntityMock>(domainEntity, dataEntity));
        }

        [TestMethod]
        public void CallDataRepositoryUpdateMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 5 };

            // Act.
            this.DomainRepository.Update(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterDirtyCalls.Single();
            this.DataRepositoryMock.AssertUpdateCalls(dataEntity);
        }

        [TestMethod]
        public void CallEntityMappertCopyPropertiesToDomainEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock() { Id = 5 };

            // Act.
            this.DomainRepository.Update(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterDirtyCalls.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDomainEntityOverrideCalls(
                new Tuple<PersonDataEntityMock, PersonDomainEntityMock>(dataEntity, domainEntity));
        }

        [TestMethod]
        public void ReturnTheSameDomainEntity_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            PersonDomainEntityMock result = this.DomainRepository.Insert(domainEntity);

            // Assert.
            Assert.AreSame(domainEntity, result);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.Insert(domainEntity);
                },
                ExecutionTimeType.Normal);
        }
    }
}