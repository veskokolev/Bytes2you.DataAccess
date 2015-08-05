using System;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.DataAccess.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class DomainRepositoryBaseMock<TDomainEntity, TDataEntity> : DomainRepositoryBase<TDomainEntity, TDataEntity, int>
        where TDomainEntity : class, IDomainEntity<int>, new()
        where TDataEntity : class, IDataEntity<int>, new()
    {
        public DomainRepositoryBaseMock(IDataRepository<TDataEntity, int> dataRepository, IEntityMapper<TDataEntity, TDomainEntity, int> entityMapper)
            : base(dataRepository, entityMapper)
        {
        }

        public void AssertDataRepositoryProperty(IDataRepository<TDataEntity, int> dataRepository)
        {
            Assert.AreSame(dataRepository, this.DataRepository);
        }

        public void AssertEntityMapperProperty(IEntityMapper<TDataEntity, TDomainEntity, int> entityMapper)
        {
            Assert.AreSame(entityMapper, this.EntityMapper);
        }
    }
}