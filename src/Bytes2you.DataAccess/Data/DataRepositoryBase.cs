using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.Data
{
    public abstract class DataRepositoryBase<TDataEntity, TId> : IDataRepository<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
    {
        private readonly IUnitOfWork<TDataEntity, TId> unitOfWork;

        public DataRepositoryBase(IUnitOfWork<TDataEntity, TId> unitOfWork)
        {
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.unitOfWork = unitOfWork;
        }

        protected IUnitOfWork<TDataEntity, TId> UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }

        public virtual TDataEntity GetById(TId id)
        {
            return this.UnitOfWork.GetById(id);
        }

        public virtual TDataEntity[] GetAll()
        {
            return this.UnitOfWork.GetAll();
        }

        public virtual int GetCount()
        {
            return this.UnitOfWork.GetCount();
        }

        public virtual TDataEntity Insert(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(entity.Id, "entity.Id").IsNotEqual(default(TId)).Throw();

            this.UnitOfWork.RegisterNew(entity);

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual TDataEntity[] InsertAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
                Guard.WhenArgument(entity.Id, "entity.Id").IsNotEqual(default(TId)).Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                this.UnitOfWork.RegisterNew(entity);
            }

            this.UnitOfWork.Commit();

            return entities.ToArray();
        }

        public virtual TDataEntity Update(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();

            this.UnitOfWork.RegisterDirty(entity);

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual TDataEntity[] UpdateAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
                Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                this.UnitOfWork.RegisterDirty(entity);
            }

            this.UnitOfWork.Commit();

            return entities.ToArray();
        }

        public virtual TDataEntity Save(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            if (EqualityComparer<TId>.Default.Equals(default(TId),entity.Id))
            {
                this.UnitOfWork.RegisterNew(entity);
            }
            else
            {
                this.UnitOfWork.RegisterDirty(entity);
            }

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual TDataEntity[] SaveAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                if (EqualityComparer<TId>.Default.Equals(default(TId),entity.Id))
                {
                    this.UnitOfWork.RegisterNew(entity);
                }
                else
                {
                    this.UnitOfWork.RegisterDirty(entity);
                }
            }

            this.UnitOfWork.Commit();

            return entities.ToArray();
        }

        public virtual void Delete(TId id)
        {
            Guard.WhenArgument(id, "id").IsEqual(default(TId)).Throw();

            TDataEntity entity = new TDataEntity();
            entity.Id = id;

            this.Delete(entity);
        }

        public virtual void Delete(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();

            this.UnitOfWork.RegisterRemoved(entity);

            this.UnitOfWork.Commit();
        }

        public virtual void DeleteAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
                Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                this.UnitOfWork.RegisterRemoved(entity);
            }

            this.UnitOfWork.Commit();
        }
    }
}