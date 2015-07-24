using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.Domain
{
    public abstract class DomainRepositoryBase<TDomainEntity, TDataEntity, TId> : IDomainRepository<TDomainEntity, TId>
        where TDomainEntity : class, IDomainEntity<TId>, new()
        where TDataEntity : class, IDataEntity<TId>, new()
    {

        public DomainRepositoryBase(IDataRepository<TDataEntity, TId> dataRepository, IEntityMapper<TDataEntity, TDomainEntity, TId> mapper)
        {
        }

        public TDomainEntity GetById(TId id)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity[] GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            throw new NotImplementedException();
        }

        public TDomainEntity Insert(TDomainEntity entity)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity[] InsertAll(IEnumerable<TDomainEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity Update(TDomainEntity entity)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity[] UpdateAll(IEnumerable<TDomainEntity> entities)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity Save(TDomainEntity entity)
        {
            throw new NotImplementedException();
        }

        public TDomainEntity[] SaveAll(IEnumerable<TDomainEntity> entities)
        {
            throw new NotImplementedException();
        }

        public void Delete(TId id)
        {
            throw new NotImplementedException();
        }

        public void Delete(TDomainEntity entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteAll(IEnumerable<TDomainEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}