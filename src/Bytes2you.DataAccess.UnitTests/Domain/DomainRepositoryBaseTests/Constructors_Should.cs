using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Domain.DomainRepositoryBaseTests
{
    [TestClass]
    public class Constructors_Should
    {
        [TestMethod]
        public void ThrowArgumentNullException_WhenDataRepositoryArgumentIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(null, new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>());
            }, "dataRepository");
        }

        [TestMethod]
        public void ThrowArgumentNullException_WhenEntityMapperIsNull()
        {
            // Act & Assert.
            Ensure.ArgumentNullExceptionIsThrown(() =>
            {
                new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(new DataRepositoryBaseMock<PersonDataEntityMock>(new UnitOfWorkMock<PersonDataEntityMock, int>()), null);
            }, "entityMapper");
        }

        [TestMethod]
        public void SetDataRepositoryProperty_WhenArgumentsAreValid()
        {
            // Arrange.
            DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(new UnitOfWorkMock<PersonDataEntityMock, int>());
            EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock> entityMapper = new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>();

            // Act.
            DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock> domainRepository =
                new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(dataRepository, entityMapper);

            // Assert.
            domainRepository.AssertDataRepositoryProperty(dataRepository);
        }

        [TestMethod]
        public void SetEntityMapperProperty_WhenArgumentsAreValid()
        {
            // Arrange.
            DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(new UnitOfWorkMock<PersonDataEntityMock, int>());
            EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock> entityMapper = new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>();

            // Act.
            DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock> domainRepository =
                new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(dataRepository, entityMapper);

            // Assert.
            domainRepository.AssertEntityMapperProperty(entityMapper);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Arrange.
            DataRepositoryBaseMock<PersonDataEntityMock> dataRepository = new DataRepositoryBaseMock<PersonDataEntityMock>(new UnitOfWorkMock<PersonDataEntityMock, int>());
            EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock> entityMapper = new EntityMapperBaseMock<PersonDataEntityMock, PersonDomainEntityMock>();

            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    new DomainRepositoryBaseMock<PersonDomainEntityMock, PersonDataEntityMock>(dataRepository, entityMapper);
                },
                ExecutionTimeType.Normal);
        }
    }
}