using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class CopyPropertiesToDataEntities_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ThrowException_WhenFromDomainEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDataEntities(null, new PersonDataEntityMock[] { });
            }, "fromDomainEntities");
        }

        [TestMethod]
        public void ThrowException_WhenToDataEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDataEntities(new PersonDomainEntityMock[] { }, null);
            }, "toDataEntities");
        }

        [TestMethod]
        public void ThrowException_WhenTheArgumentsAreWithDifferentLength()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDataEntities(domainEntities, dataEntities);
            }, "fromDomainEntities.Length");
        }

        [TestMethod]
        public void CallCopyPropertiesToDataEntityOverride_WhenArgumentsAreValid()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            // Act.
            this.EntityMapperBaseMock.CopyPropertiesToDataEntities(domainEntities, dataEntities);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDataEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(domainEntities, dataEntities));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.CopyPropertiesToDataEntities(domainEntities, dataEntities);
                },
                ExecutionTimeType.Normal);
        }
    }
}