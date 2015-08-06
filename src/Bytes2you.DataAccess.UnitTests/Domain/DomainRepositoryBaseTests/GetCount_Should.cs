using System;
using System.Linq;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class GetCount_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallDataRepositoryGetCountMethod()
        {
            // Act.
            this.DomainRepository.GetCount();

            // Asert.
            this.DataRepositoryMock.AssertGetCountCallCount(1);
        }

        [TestMethod]
        public void ReturnsTheValueOfDataRepositoryGetCountMethod()
        {
            // Act.
            int count = this.DomainRepository.GetCount();
            
            // Asert.
            this.UnitOfWorkMock.AssertGetCountReturnValues(count);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.GetCount();
                },
                ExecutionTimeType.Normal);
        }
    }
}