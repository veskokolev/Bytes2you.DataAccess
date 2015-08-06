using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class Constructors_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenUnitOfWorkArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                new DataRepositoryBaseMock<PersonDataEntityMock>(null);
            }, "unitOfWork");
        }

        [TestMethod]
        public void SetUnitOfWorkProperty_WhenArgumentsAreValid()
        {
            // Arrange.
            UnitOfWorkMock<PersonDataEntityMock, int> unitOfWorkMock = new UnitOfWorkMock<PersonDataEntityMock, int>();

            // Act.
            DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(unitOfWorkMock);

            // Assert.
            dataRepository.AssertUnitOfWorkProperty(unitOfWorkMock);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            UnitOfWorkMock<PersonDataEntityMock, int> unitOfWorkMock = new UnitOfWorkMock<PersonDataEntityMock, int>();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(unitOfWorkMock);
                },
                ExecutionTimeType.Normal);
        }
    }
}