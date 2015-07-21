using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.EfUnitOfWorkTests
{
    [TestClass]
    public class Count_Should : EfUnitOfWorkTestBase
    {
        [TestMethod]
        public void CallDbSetCountMethodAndReturnItsResult()
        {
            List<PersonDataEntityMock> entities = new List<PersonDataEntityMock>();

            for (int i = 1; i <= 50; i++)
            {
                // Arrange.
                PersonDataEntityMock entity = new PersonDataEntityMock() { Id = i };
                this.DbContextMock.MockSet<PersonDataEntityMock>().Add(entity);
                entities.Add(entity);

                // Act.
                int resultCount = this.EfUnitOfWork.GetCount<PersonDataEntityMock, int>();

                // Assert.
                Assert.AreEqual(entities.Count(), resultCount);
            }
        }
    }
}