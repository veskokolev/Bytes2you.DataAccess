using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class GetAll_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallDataRepositoryGetAllMethod()
        {
            // Act.
            this.DomainRepository.GetAll();

            // Asert.
            this.DataRepositoryMock.AssertGetAllCallCount(1);
        }

        [TestMethod]
        public void MapTheReturnedDataEntitiesToDomainEntitiesAndReturnThem()
        {
            // Act.
            PersonDomainEntityMock[] personDomainEntityMocks = this.DomainRepository.GetAll();
            
            // Asert.
            PersonDataEntityMock[] personDataEntityMocks = this.UnitOfWorkMock.GetAllReturnValues.Single();
            this.EntityMapperMock.AssertCopyPropertiesToDomainEntityOverrideCalls(TupleHelper.CombineIntoTupleArray(personDataEntityMocks, personDomainEntityMocks));
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.GetAll();
                },
                ExecutionTimeType.Normal);
        }
    }
}