using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.DataAccess.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class EntityMapperBaseMock<TDataEntity, TDomainEntity> : EntityMapperBase<TDataEntity, TDomainEntity, int>
        where TDataEntity : class, IDataEntity<int>, new()
        where TDomainEntity : class, IDomainEntity<int>, new()
    {
        private readonly List<Tuple<TDomainEntity, TDataEntity>> copyPropertiesToDataEntityOverrideCalls;
        private readonly List<Tuple<TDataEntity, TDomainEntity>> copyPropertiesToDomainEntityOverrideCalls;

        public EntityMapperBaseMock()
        {
            this.copyPropertiesToDataEntityOverrideCalls = new List<Tuple<TDomainEntity, TDataEntity>>();
            this.copyPropertiesToDomainEntityOverrideCalls = new List<Tuple<TDataEntity, TDomainEntity>>();
        }

        protected override void CopyPropertiesToDataEntityOverride(TDomainEntity fromDomainEntity, TDataEntity toDataEntity)
        {
            this.copyPropertiesToDataEntityOverrideCalls.Add(new Tuple<TDomainEntity, TDataEntity>(fromDomainEntity, toDataEntity));

            toDataEntity.Id = fromDomainEntity.Id;
        }

        protected override void CopyPropertiesToToDomainEntity(TDataEntity fromDataEntity, TDomainEntity toDomainEntity)
        {
            this.copyPropertiesToDomainEntityOverrideCalls.Add(new Tuple<TDataEntity, TDomainEntity>(fromDataEntity, toDomainEntity));

            toDomainEntity.Id = fromDataEntity.Id;
        }

        public void AssertCopyPropertiesToDataEntityOverrideCalls(params Tuple<TDomainEntity, TDataEntity>[] callArguments)
        {
            CollectionAssert.AreEqual(this.copyPropertiesToDataEntityOverrideCalls, callArguments);
        }

        public void AssertCopyPropertiesToDomainEntityOverrideCalls(params Tuple<TDataEntity, TDomainEntity>[] callArguments)
        {
            CollectionAssert.AreEqual(this.copyPropertiesToDomainEntityOverrideCalls, callArguments);
        }
    }
}