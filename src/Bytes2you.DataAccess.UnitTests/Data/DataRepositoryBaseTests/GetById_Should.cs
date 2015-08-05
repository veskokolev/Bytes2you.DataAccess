using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class GetById_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallUnitOfWorkGetByIdMethodAndReturnItsResult()
        {
            // Arrange.
            int id = 3;

            // Act.
            PersonDataEntityMock returnValue = this.DataRepository.GetById(id);
            
            // Assert.
            this.UnitOfWorkMock.AssertGetByIdCalls(id);
            this.UnitOfWorkMock.AssertGetByIdReturnValues(returnValue);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            int id = 3;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.GetById(id);
                },
                ExecutionTimeType.Fast);
        }
    }
}