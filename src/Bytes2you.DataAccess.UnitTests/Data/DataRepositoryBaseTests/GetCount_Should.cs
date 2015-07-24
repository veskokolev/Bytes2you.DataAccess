using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class GetCount_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallUnitOfWorkGetCountMethodAndReturnItsResult()
        {
            // Act.
            int returnValue = this.DataRepository.GetCount();

            // Assert.
            this.UnitOfWorkMock.AssertGetCountReturnValues(returnValue);
        }
    }
}