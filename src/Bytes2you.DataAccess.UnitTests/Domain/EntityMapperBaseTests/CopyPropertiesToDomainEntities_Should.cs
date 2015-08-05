using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class CopyPropertiesToDomainEntities_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ThrowException_WhenFromDoataEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDomainEntities(null, new PersonDomainEntityMock[] { });
            }, "fromDataEntities");
        }

        [TestMethod]
        public void ThrowException_WhenToDomainEntitiesArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDomainEntities(new PersonDataEntityMock[] { }, null);
            }, "toDomainEntities");
        }

        [TestMethod]
        public void ThrowException_WhenTheArgumentsAreWithDifferentLength()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act & Assert.
            Ensure.ArgumentExceptionIsThrown(() =>
            {
                this.EntityMapperBaseMock.CopyPropertiesToDomainEntities(dataEntities, domainEntities);
            }, "fromDataEntities.Length");
        }

        [TestMethod]
        public void CallCopyPropertiesToDomainEntityOverride_WhenArgumentsAreValid()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act.
            this.EntityMapperBaseMock.CopyPropertiesToDomainEntities(dataEntities, domainEntities);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDomainEntityOverrideCalls(
                TupleHelper.CombineIntoTupleArray(dataEntities, domainEntities));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[]
                {
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            PersonDomainEntityMock[] domainEntities = 
                new PersonDomainEntityMock[] 
                { 
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock(),
                    new PersonDomainEntityMock()
                };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.CopyPropertiesToDomainEntities(dataEntities, domainEntities);
                },
                ExecutionTimeType.Normal);
        }
    }
}