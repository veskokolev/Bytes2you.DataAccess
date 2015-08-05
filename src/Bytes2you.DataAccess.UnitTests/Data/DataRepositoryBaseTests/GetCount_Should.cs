using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
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

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.GetCount();
                },
                ExecutionTimeType.Fast);
        }
    }
}