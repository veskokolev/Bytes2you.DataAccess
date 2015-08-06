using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class Insert_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.DomainRepository.Insert(null);
            }, "entity");
        }

        [TestMethod]
        public void CallEntityMapperToDataEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            this.DomainRepository.Insert(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterNewCalls.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                new Tuple<PersonDomainEntityMock, PersonDataEntityMock>(domainEntity, dataEntity));
        }

        [TestMethod]
        public void CallDataRepositoryInsertMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            this.DomainRepository.Insert(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterNewCalls.Single();
            this.DataRepositoryMock.AssertInsertCalls(dataEntity);
        }

        [TestMethod]
        public void CallEntityMappertCopyPropertiesToDomainEntityMethod_WhenEntityArgumentIsValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            this.DomainRepository.Insert(domainEntity);

            // Assert.
            PersonDataEntityMock dataEntity = this.UnitOfWorkMock.RegisterNewCalls.Single();
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
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();
                    this.DomainRepository.Insert(domainEntity);
                },
                ExecutionTimeType.Normal);
        }
    }
}