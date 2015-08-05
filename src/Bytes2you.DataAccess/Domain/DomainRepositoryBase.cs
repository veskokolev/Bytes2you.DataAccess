using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.Domain
{
    public abstract class DomainRepositoryBase<TDomainEntity, TDataEntity, TId> : IDomainRepository<TDomainEntity, TId>
        where TDomainEntity : class, IDomainEntity<TId>, new()
        where TDataEntity : class, IDataEntity<TId>, new()
    {
        private readonly IDataRepository<TDataEntity, TId> dataRepository;
        private readonly IEntityMapper<TDataEntity, TDomainEntity, TId> entityMapper;

        public DomainRepositoryBase(IDataRepository<TDataEntity, TId> dataRepository, IEntityMapper<TDataEntity, TDomainEntity, TId> entityMapper)
        {
            Guard.WhenArgument(dataRepository, "dataRepository").IsNull().Throw();
            Guard.WhenArgument(entityMapper, "entityMapper").IsNull().Throw();

            this.dataRepository = dataRepository;
            this.entityMapper = entityMapper;
        }

        protected IDataRepository<TDataEntity, TId> DataRepository
        {
            get
            {
                return this.dataRepository;
            }
        }

        protected IEntityMapper<TDataEntity, TDomainEntity, TId> EntityMapper
        {
            get
            {
                return this.entityMapper;
            }
        }

        public virtual TDomainEntity GetById(TId id)
        {
            TDataEntity dataEntity = this.dataRepository.GetById(id);

            TDomainEntity domainEntity = new TDomainEntity();
            this.entityMapper.CopyPropertiesToDomainEntity(dataEntity, domainEntity);

            return domainEntity;
        }

        public virtual TDomainEntity[] GetAll()
        {
            TDataEntity[] dataEntities = this.dataRepository.GetAll();

            return this.entityMapper.ToDomainEntities(dataEntities);
        }

        public virtual int GetCount()
        {
            return this.dataRepository.GetCount();
        }

        public virtual TDomainEntity Insert(TDomainEntity entity)
        {
            return this.ConvertAndExecute(entity, (e) => this.dataRepository.Insert(e));
        }

        public virtual TDomainEntity[] InsertAll(IEnumerable<TDomainEntity> entities)
        {
            return this.ConvertAndExecute(entities, (e) => this.dataRepository.InsertAll(e));
        }

        public virtual TDomainEntity Update(TDomainEntity entity)
        {
            return this.ConvertAndExecute(entity, (e) => this.dataRepository.Update(e));
        }

        public virtual TDomainEntity[] UpdateAll(IEnumerable<TDomainEntity> entities)
        {
            return this.ConvertAndExecute(entities, (e) => this.dataRepository.UpdateAll(e));
        }

        public virtual TDomainEntity Save(TDomainEntity entity)
        {
            return this.ConvertAndExecute(entity, (e) => this.dataRepository.Save(e));
        }

        public virtual TDomainEntity[] SaveAll(IEnumerable<TDomainEntity> entities)
        {
            return this.ConvertAndExecute(entities, (e) => this.dataRepository.SaveAll(e));
        }

        public virtual void Delete(TId id)
        {
            this.dataRepository.Delete(id);
        }

        public virtual void Delete(TDomainEntity entity)
        {
            this.ConvertAndExecute(entity, (e) => this.dataRepository.Delete(e));
        }

        public virtual void DeleteAll(IEnumerable<TDomainEntity> entities)
        {
            this.ConvertAndExecute(entities, (e) => this.dataRepository.DeleteAll(e));
        }

        protected virtual TDomainEntity ConvertAndExecute(TDomainEntity entity, Func<TDataEntity, TDataEntity> function)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(function, "function").IsNull().Throw();
            
            TDataEntity dataEntity = this.entityMapper.ToDataEntity(entity);
            dataEntity = function(dataEntity);
            this.entityMapper.CopyPropertiesToDomainEntity(dataEntity, entity);

            return entity;
        }

        protected virtual void ConvertAndExecute(TDomainEntity entity, Action<TDataEntity> action)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(action, "action").IsNull().Throw();

            this.ConvertAndExecute(entity, (e) => { action(e); return e; });
        }

        protected virtual TDomainEntity[] ConvertAndExecute(IEnumerable<TDomainEntity> entities, Func<TDataEntity[], TDataEntity[]> function)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();
            Guard.WhenArgument(function, "function").IsNull().Throw();

            foreach (TDomainEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
            }

            TDomainEntity[] domainEntities = entities.ToArray();

            TDataEntity[] dataEntities = this.entityMapper.ToDataEntities(domainEntities);
            dataEntities = function(dataEntities);
            this.entityMapper.CopyPropertiesToDomainEntities(dataEntities, domainEntities);

            return domainEntities;
        }

        protected virtual void ConvertAndExecute(IEnumerable<TDomainEntity> entities, Action<TDataEntity[]> action)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();
            Guard.WhenArgument(action, "action").IsNull().Throw();

            this.ConvertAndExecute(entities, (e) => { action(e); return e; });
        }
    }
}