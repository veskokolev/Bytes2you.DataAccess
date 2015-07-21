using System;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class GetById_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void CallDbSetFindMethodWithTheGivenIdAndReturnItsResult()
        {
            // Arrange.
            int id = 3;

            // Act.
            PersonDataEntityMock resultEntity = this.EfUnitOfWork.GetById<PersonDataEntityMock, int>(id);

            // Assert.
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertFindCalls(id);
            this.DbContextMock.MockSet<PersonDataEntityMock>().AssertFindReturnValues(resultEntity);
        }
    }
}