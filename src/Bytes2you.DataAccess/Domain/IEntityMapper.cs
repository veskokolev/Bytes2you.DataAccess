using System;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.Domain
{
    public interface IEntityMapper<TDataEntity, TDomainEntity, TId>
        where TDataEntity : IDataEntity<TId>
        where TDomainEntity : IDomainEntity<TId>
    {
        void ToDataEntity(TDomainEntity fromDomainEntity, TDataEntity toDataEntity);
        void ToDomainEntity(TDataEntity fromDataEntity, TDomainEntity toDomainEntity);
    }
}