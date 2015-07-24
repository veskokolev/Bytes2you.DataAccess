using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfRepositoryTests
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
                new EfRepositoryMock<PersonDataEntityMock, int>(null);
            }, "unitOfWork");
        }

        [TestMethod]
        public void SetAllGivenProperties()
        {
            // Arrange.
            DbContextMock dbContextMock = new DbContextMock();
            EfUnitOfWork<PersonDataEntityMock, int> unitOfWorkMock = new EfUnitOfWork<PersonDataEntityMock, int>(dbContextMock);

            // Act.
            EfRepositoryMock<PersonDataEntityMock, int> dataRepository = new EfRepositoryMock<PersonDataEntityMock, int>(unitOfWorkMock);

            // Assert.
            Assert.AreSame(unitOfWorkMock, ((IEfRepository<PersonDataEntityMock, int>)dataRepository).UnitOfWork);
        }
    }
}