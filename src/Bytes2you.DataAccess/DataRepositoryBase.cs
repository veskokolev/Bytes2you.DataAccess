using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bytes2you.DataAccess
{
    public abstract class DataRepositoryBase<TDataEntity, TId, TUnitOfWork> : IRepository<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>, new()
        where TUnitOfWork : class, IUnitOfWork
    {
        private readonly TUnitOfWork unitOfWork;

        public DataRepositoryBase(TUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(unitOfWork, "unitOfWork").IsNull().Throw();

            this.unitOfWork = unitOfWork;
        }

        protected TUnitOfWork UnitOfWork
        {
            get
            {
                return this.unitOfWork;
            }
        }

        public virtual TDataEntity GetById(TId id)
        {
            return this.UnitOfWork.GetById<TDataEntity, TId>(id);
        }

        public virtual IList<TDataEntity> GetAll()
        {
            return this.UnitOfWork.GetAll<TDataEntity, TId>();
        }

        public virtual int GetCount()
        {
            return this.UnitOfWork.GetCount<TDataEntity, TId>();
        }

        public virtual TDataEntity Insert(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(entity.Id, "entity.Id").IsNotEqual(default(TId)).Throw();

            this.UnitOfWork.RegisterNew<TDataEntity, TId>(entity);

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual IList<TDataEntity> InsertAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
                Guard.WhenArgument(entity.Id, "entity.Id").IsNotEqual(default(TId)).Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                this.UnitOfWork.RegisterNew<TDataEntity, TId>(entity);
            }

            this.UnitOfWork.Commit();

            return entities.ToList();
        }

        public virtual TDataEntity Update(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();
            Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();

            this.UnitOfWork.RegisterDirty<TDataEntity, TId>(entity);

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual IList<TDataEntity> UpdateAll(IEnumerable<TDataEntity> entities)
        {
            Guard.WhenArgument(entities, "entities").IsNull().Throw();

            foreach (TDataEntity entity in entities)
            {
                Guard.WhenArgument(entity, "entity").IsNull().Throw();
                Guard.WhenArgument(entity.Id, "entity.Id").IsEqual(default(TId)).Throw();
            }

            foreach (TDataEntity entity in entities)
            {
                this.UnitOfWork.RegisterDirty<TDataEntity, TId>(entity);
            }

            this.UnitOfWork.Commit();

            return entities.ToList();
        }

        public virtual TDataEntity Save(TDataEntity entity)
        {
            Guard.WhenArgument(entity, "entity").IsNull().Throw();

            if (EqualityComparer<TId>.Default.Equals(default(TId),entity.Id))
            {
                this.UnitOfWork.RegisterNew<TDataEntity, TId>(entity);
            }
            else
            {
                this.UnitOfWork.RegisterDirty<TDataEntity, TId>(entity);
            }

            this.UnitOfWork.Commit();

            return entity;
        }

        public virtual IList<TDataEntity> SaveAll(IEnumerable<TDataEntity> entities)
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
                    this.UnitOfWork.RegisterNew<TDataEntity, TId>(entity);
                }
                else
                {
                    this.UnitOfWork.RegisterDirty<TDataEntity, TId>(entity);
                }
            }

            this.UnitOfWork.Commit();

            return entities.ToList();
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

            this.UnitOfWork.RegisterRemoved<TDataEntity, TId>(entity);

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
                this.UnitOfWork.RegisterRemoved<TDataEntity, TId>(entity);
            }

            this.UnitOfWork.Commit();
        }
    }
}