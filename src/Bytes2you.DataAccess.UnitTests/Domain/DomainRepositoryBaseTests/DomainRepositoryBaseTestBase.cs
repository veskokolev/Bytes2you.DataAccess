using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    public abstract class DomainRepositoryBaseTestBase
    {
        protected DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock> DomainRepository { get; private set; }
        protected UnitOfWorkMock<PersonDataEntityMock, int> UnitOfWorkMock { get; private set; }
        protected DataRepositoryBaseMock<PersonDataEntityMock> DataRepositoryMock { get; private set; }
        protected EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock> EntityMapperMock { get; private set; }

        [TestInitialize]
        public void Initialize()
        {
            // Arrange.
            this.UnitOfWorkMock = new UnitOfWorkMock<PersonDataEntityMock, int>();
            this.DataRepositoryMock = new DataRepositoryBaseMock<PersonDataEntityMock>(this.UnitOfWorkMock);
            this.EntityMapperMock = new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>();
            this.DomainRepository = new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(this.DataRepositoryMock, this.EntityMapperMock);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.UnitOfWorkMock = null;
            this.DataRepositoryMock = null;
            this.EntityMapperMock = null;
            this.DomainRepository = null;
        }
    }
}