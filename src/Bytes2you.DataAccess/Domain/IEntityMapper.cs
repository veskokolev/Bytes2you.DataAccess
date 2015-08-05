using System;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.Domain
{
    public interface IEntityMapper<TDataEntity, TDomainEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
        where TDomainEntity : class, IDomainEntity<TId>, new()
    {
        void CopyPropertiesToDataEntity(TDomainEntity fromDomainEntity, TDataEntity toDataEntity);
        void CopyPropertiesToDomainEntity(TDataEntity fromDataEntity, TDomainEntity toDomainEntity);

        void CopyPropertiesToDataEntities(TDomainEntity[] fromDomainEntities, TDataEntity[] toDataEntities);
        void CopyPropertiesToDomainEntities(TDataEntity[] fromDataEntities, TDomainEntity[] toDomainEntities);

        TDataEntity ToDataEntity(TDomainEntity fromDomainEntity);
        TDomainEntity ToDomainEntity(TDataEntity fromDataEntity);

        TDataEntity[] ToDataEntities(TDomainEntity[] fromDomainEntities);
        TDomainEntity[] ToDomainEntities(TDataEntity[] fromDataEntities);
    }
}