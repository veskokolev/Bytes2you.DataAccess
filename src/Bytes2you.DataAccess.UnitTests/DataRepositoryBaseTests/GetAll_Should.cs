using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.DataRepositoryBaseTests
{
    [TestClass]
    public class GetAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallUnitOfWorkGetAllMethodAndReturnItsResult()
        {
            // Act.
            object returnValue = this.DataRepository.GetAll();
            
            // Assert.
            this.UnitOfWorkMock.AssertGetAllReturnValues(returnValue);
        }
    }
}