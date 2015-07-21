using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.DataRepositoryBaseTests
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
    }
}