using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class GetAll_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void CallDbSetAsNoTrackingMethodAndReturnItsResult()
        {
            // Arrange.
            PersonDataEntityMock[] entities = new PersonDataEntityMock[]
            {
                new PersonDataEntityMock() { Id = 3 },
                new PersonDataEntityMock() { Id = 4 },
                new PersonDataEntityMock() { Id = 5 }
            };

            this.DbContextMock.MockSet<PersonDataEntityMock>().AddRange(entities);

            // Act.
            PersonDataEntityMock[] resultEntities = this.EfUnitOfWork.GetAll();

            // Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertAsNoTrackingCallCount(1);
            CollectionAssert.AreEqual(entities, resultEntities);
        }
    }
}