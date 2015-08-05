using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    [TestClass]
    public class ToDomainEntities_Should : EntityMapperBaseTestBase
    {
        [TestMethod]
        public void ReturnNull_WhenFromDataEntitiesArgumentIsNull()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = null;

            // Act.
            PersonDomainEntityMock[] domainEntities = this.EntityMapperBaseMock.ToDomainEntities(dataEntities);

            // Assert.
            Assert.IsNull(domainEntities);
        }

        [TestMethod]
        public void NotCallCopyPropertiesToDomainEntityOverride_WhenFromDataEntitiesArgumentIsNull()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = null;

            // Act.
            PersonDomainEntityMock[] domainEntities = this.EntityMapperBaseMock.ToDomainEntities(dataEntities);

            // Assert.
            this.EntityMapperBaseMock.AssertCopyPropertiesToDomainEntityOverrideCalls();
        }
        
        [TestMethod]
        public void CallCopyPropertiesToDomainEntityOverrideAndReturnItsResults_WhenFromDataEntitiesArgumentIsNotNull()
        {
            // Arrange.
            PersonDataEntityMock[] dataEntities = 
                new PersonDataEntityMock[] 
                { 
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock(),
                    new PersonDataEntityMock()
                };

            // Act.
            PersonDomainEntityMock[] domainEntities = this.EntityMapperBaseMock.ToDomainEntities(dataEntities);

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
                    new PersonDataEntityMock()
                };

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.EntityMapperBaseMock.ToDomainEntities(dataEntities);
                },
                ExecutionTimeType.Fast);
        }
    }
}