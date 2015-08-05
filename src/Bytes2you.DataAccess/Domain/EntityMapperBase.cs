using System;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.Domain
{
    public abstract class EntityMapperBase<TDataEntity, TDomainEntity, TId> : IEntityMapper<TDataEntity, TDomainEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
        where TDomainEntity : class, IDomainEntity<TId>, new()
    {
        public void CopyPropertiesToDataEntity(TDomainEntity fromDomainEntity, TDataEntity toDataEntity)
        {
            Guard.WhenArgument(fromDomainEntity, "fromDomainEntity").IsNull().Throw();
            Guard.WhenArgument(toDataEntity, "toDataEntity").IsNull().Throw();

            this.CopyPropertiesToDataEntityOverride(fromDomainEntity, toDataEntity);
        }

        public void CopyPropertiesToDomainEntity(TDataEntity fromDataEntity, TDomainEntity toDomainEntity)
        {
            Guard.WhenArgument(fromDataEntity, "fromDataEntity").IsNull().Throw();
            Guard.WhenArgument(toDomainEntity, "toDomainEntity").IsNull().Throw();

            this.CopyPropertiesToToDomainEntity(fromDataEntity, toDomainEntity);
        }

        public void CopyPropertiesToDataEntities(TDomainEntity[] fromDomainEntities, TDataEntity[] toDataEntities)
        {
            Guard.WhenArgument(fromDomainEntities, "fromDomainEntities").IsNull().Throw();
            Guard.WhenArgument(toDataEntities, "toDataEntities").IsNull().Throw();
            Guard.WhenArgument(fromDomainEntities.Length, "fromDomainEntities.Length").IsNotEqual(toDataEntities.Length).Throw();

            for (int i = 0; i < fromDomainEntities.Length; i++)
            {
                this.CopyPropertiesToDataEntity(fromDomainEntities[i], toDataEntities[i]);
            }
        }

        public void CopyPropertiesToDomainEntities(TDataEntity[] fromDataEntities, TDomainEntity[] toDomainEntities)
        {
            Guard.WhenArgument(fromDataEntities, "fromDataEntities").IsNull().Throw();
            Guard.WhenArgument(toDomainEntities, "toDomainEntities").IsNull().Throw();
            Guard.WhenArgument(fromDataEntities.Length, "fromDataEntities.Length").IsNotEqual(toDomainEntities.Length).Throw();

            for (int i = 0; i < fromDataEntities.Length; i++)
            {
                this.CopyPropertiesToDomainEntity(fromDataEntities[i], toDomainEntities[i]);
            }
        }

        public TDataEntity ToDataEntity(TDomainEntity fromDomainEntity)
        {
            if (fromDomainEntity == null)
            {
                return null;
            }

            TDataEntity toDataEntity = new TDataEntity();
            this.CopyPropertiesToDataEntityOverride(fromDomainEntity, toDataEntity);

            return toDataEntity;
        }

        public TDomainEntity ToDomainEntity(TDataEntity fromDataEntity)
        {
            if (fromDataEntity == null)
            {
                return null;
            }

            TDomainEntity toDomainEntity = new TDomainEntity();
            this.CopyPropertiesToToDomainEntity(fromDataEntity, toDomainEntity);

            return toDomainEntity;
        }

        public TDataEntity[] ToDataEntities(TDomainEntity[] fromDomainEntities)
        {
            if (fromDomainEntities == null)
            {
                return null;
            }

            return fromDomainEntities.Select(e => this.ToDataEntity(e)).ToArray();
        }

        public TDomainEntity[] ToDomainEntities(TDataEntity[] fromDataEntities)
        {
            if (fromDataEntities == null)
            {
                return null;
            }

            return fromDataEntities.Select(e => this.ToDomainEntity(e)).ToArray();
        }

        protected abstract void CopyPropertiesToDataEntityOverride(TDomainEntity fromDomainEntity,TDataEntity toDataEntity);
        protected abstract void CopyPropertiesToToDomainEntity(TDataEntity fromDataEntity,TDomainEntity toDomainEntity);
    }
}