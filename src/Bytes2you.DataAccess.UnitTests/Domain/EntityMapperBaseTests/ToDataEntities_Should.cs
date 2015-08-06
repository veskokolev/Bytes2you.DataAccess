using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class ToDataEntities_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ReturnNull_WhenFromDomainEntitiesArgumentIsNull()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities = null;

            // Act.
            PersonDataEntityMock[] dataEntities = this.EntityMapperBaseMock.ToDataEntities(domainEntities);

            // Assert.
            Assert.IsNull(dataEntities);
        }

        [TestMethod]
        public void NotCallCopyPropertiesToDataEntityOverride_WhenFromDomainEntitiesArgumentIsNull()
        {
            // Arrange.
            PersonDomainEntityMock[] domainEntities = null;

            // Act.
            PersonDataEntityMock[] dataEntities = this.EntityMapperBaseMock.ToDataEntities(domainEntities);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDataEntityOverrideCalls();
        }
        
        [TestMethod]
        public void CallCopyPropertiesToDataEntityOverrideAndReturnItsResults_WhenFromDomainEntitiesArgumentIsNotNull()
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
            PersonDataEntityMock[] dataEntities = this.EntityMapperBaseMock.ToDataEntities(domainEntities);

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
                    new PersonDomainEntityMock()
                };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.ToDataEntities(domainEntities);
                },
                ExecutionTimeType.Fast);
        }
    }
}