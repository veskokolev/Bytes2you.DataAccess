using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class GetById_Should : DomainRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallDataRepositoryGetByIdMethodWithTheGivenIdArgument()
        {
            // Arrange.
            int id = 5;

            // Act.
            this.DomainRepository.GetById(id);

            // Asert.
            this.DataRepositoryMock.AssertGetByIdCalls(id);
        }

        [TestMethod]
        public void MapTheReturnedDataEntityToDomainEntityAndReturnIt()
        {
            // Arrange.
            int id = 5;

            // Act.
            PersonDomainEntityMock personDomainEntityMock = this.DomainRepository.GetById(id);

            // Asert.
            this.UnitOfWorkMock.AssertGetByIdCalls(id);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            int id = 5;

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DomainRepository.GetById(id);
                },
                ExecutionTimeType.Normal);
        }
    }
}