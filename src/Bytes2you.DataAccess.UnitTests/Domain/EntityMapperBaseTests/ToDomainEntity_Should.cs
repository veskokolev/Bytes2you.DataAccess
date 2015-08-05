using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class ToDomainEntity_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ReturnNull_WhenFromDataEntityArgumentIsNull()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = null;

            // Act.
            PersonDomainEntityMock domainEntity = this.EntityMapperBaseMock.ToDomainEntity(dataEntity);

            // Assert.
            Assert.IsNull(domainEntity);
        }

        [TestMethod]
        public void NotCallCopyPropertiesToDomainEntityOverride_WhenFromDataEntityArgumentIsNull()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = null;

            // Act.
            PersonDomainEntityMock domainEntity = this.EntityMapperBaseMock.ToDomainEntity(dataEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDomainEntityOverrideCalls();
        }

        [TestMethod]
        public void CallCopyPropertiesToDomainEntityOverrideAndReturnItsResult_WhenFromDataEntityArgumentIsNotNull()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();

            // Act.
            PersonDomainEntityMock domainEntity = this.EntityMapperBaseMock.ToDomainEntity(dataEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDomainEntityOverrideCalls(new Tuple<PersonDataEntityMock, PersonDomainEntityMock>(dataEntity, domainEntity));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.ToDomainEntity(dataEntity);
                },
                ExecutionTimeType.SuperFast);
        }
    }
}