using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.DataRepositoryBaseTests
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
        public void SetAllGivenProperties()
        {
            // Arrange.
            UnitOfWorkMock unitOfWorkMock = new UnitOfWorkMock();

            // Act.
            DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(unitOfWorkMock);

            // Assert.
            dataRepository.AssertUnitOfWorkProperty(unitOfWorkMock);
        }
    }
}