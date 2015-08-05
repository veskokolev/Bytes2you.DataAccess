using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class CopyPropertiesToDataEntity_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ThrowException_WhenFromDomainEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDataEntity(null, new PersonDataEntityMock());
            }, "fromDomainEntity");
        }

        [TestMethod]
        public void ThrowException_WhenToDataEntityArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDataEntity(new PersonDomainEntityMock(), null);
            }, "toDataEntity");
        }

        [TestMethod]
        public void CallCopyPropertiesToDataEntityOverride_WhenArgumentsAreValid()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();

            // Act.
            this.EntityMapperBaseMock.CopyPropertiesToDataEntity(domainEntity, dataEntity);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDataEntityOverrideCalls(new Tuple<PersonDomainEntityMock, PersonDataEntityMock>(domainEntity, dataEntity));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDomainEntityMock domainEntity = new PersonDomainEntityMock();
            PersonDataEntityMock dataEntity = new PersonDataEntityMock();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.CopyPropertiesToDataEntity(domainEntity, dataEntity);
                },
                ExecutionTimeType.Fast);
        }
    }
}