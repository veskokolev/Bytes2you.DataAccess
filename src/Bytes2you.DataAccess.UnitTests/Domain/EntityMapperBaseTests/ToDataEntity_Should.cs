using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class ToDataEntity_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ReturnNull_WhenFromDomainEntityArgumentIsNull()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = null;

            // Act.
            PersonDataEntityMock dataEntity = this.EntityMapperBaseMock.ToDataEntity(domainEntity);

            // Assert.
            Assert.IsNull(dataEntity);
        }

        [TestMethod]
        public void NotCallCopyPropertiesToDataEntityOverride_WhenFromDomainEntityArgumentIsNull()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = null;

            // Act.
            PersonDataEntityMock dataEntity = this.EntityMapperBaseMock.ToDataEntity(domainEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDataEntityOverrideCalls();
        }
        
        [TestMethod]
        public void CallCopyPropertiesToDataEntityOverrideAndReturnItsResult_WhenFromDomainEntityArgumentIsNotNull()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            PersonDataEntityMock dataEntity = this.EntityMapperBaseMock.ToDataEntity(domainEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDataEntityOverrideCalls(new Tuple<PersonDomainEntityMock, PersonDataEntityMock>(domainEntity, dataEntity));
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
                    this.EntityMapperBaseMock.ToDataEntity(domainEntity);
                },
                ExecutionTimeType.SuperFast);
        }
    }
}