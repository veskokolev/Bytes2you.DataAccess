using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class CopyPropertiesToDomainEntity_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ThrowException_WhenFromDataEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDomainEntity(null, new PersonDomainEntityMock());
            }, "fromDataEntity");
        }

        [TestMethod]
        public void ThrowException_WhenToDomainEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDomainEntity(new PersonDataEntityMock(), null);
            }, "toDomainEntity");
        }

        [TestMethod]
        public void CallCopyPropertiesToDomainEntityOverride_WhenArgumentsAreValid()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act.
            this.EntityMapperBaseMock.CopyPropertiesToDomainEntity(dataEntity, domainEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDomainEntityOverrideCalls(new Tuple<PersonDataEntityMock, PersonDomainEntityMock>(dataEntity, domainEntity));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.CopyPropertiesToDomainEntity(dataEntity, domainEntity);
                },
                ExecutionTimeType.Fast);
        }
    }
}