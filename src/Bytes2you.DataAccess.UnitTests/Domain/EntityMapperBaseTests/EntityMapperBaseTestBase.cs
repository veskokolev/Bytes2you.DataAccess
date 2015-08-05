using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.EntityMapperBaseTests
{
    public abstract class EntityMapperBaseTestBase
    {
        protected EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock> EntityMapperBaseMock { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange.
            this.EntityMapperBaseMock = new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>();
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.EntityMapperBaseMock = null;
        }
    }
}