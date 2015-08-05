using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class UnitOfWorkMock<TDataEntity, TId> : IUnitOfWork<TDataEntity, TId>
        where TDataEntity : class, IDataEntity<TId>
    {
        private readonly List<TId> getByIdCalls;
        private readonly List<TDataEntity> getByIdReturnValues;
        private readonly List<TDataEntity[]> getAllReturnValues;
        private readonly List<int> getCountReturnValues;
        private readonly List<TDataEntity> registerNewCalls;
        private readonly List<TDataEntity> registerDirtyCalls;
        private readonly List<TDataEntity> registerRemovedCalls;

        public UnitOfWorkMock()
        {
            this.getByIdCalls = new List<TId>();
            this.getByIdReturnValues = new List<TDataEntity>();
            this.getAllReturnValues = new List<TDataEntity[]>();
            this.getCountReturnValues = new List<int>();
            this.registerNewCalls = new List<TDataEntity>();
            this.registerDirtyCalls = new List<TDataEntity>();
            this.registerRemovedCalls = new List<TDataEntity>();
        }

        public IEnumerable<TDataEntity[]> GetAllReturnValues
        {
            get
            {
                return this.getAllReturnValues;
            }
        }

        public IEnumerable<TDataEntity> RegisterNewCalls
        {
            get
            {
                return this.registerNewCalls;
            }
        }

        public IEnumerable<TDataEntity> RegisterDirtyCalls
        {
            get
            {
                return this.registerDirtyCalls;
            }
        }

        public IEnumerable<TDataEntity> RegisterRemovedCalls
        {
            get
            {
                return this.registerRemovedCalls;
            }
        }

        public TDataEntity GetById(TId id)
        {
            TDataEntity result = (TDataEntity)Activator.CreateInstance(typeof(TDataEntity));

            this.getByIdCalls.Add(id);
            this.getByIdReturnValues.Add(result);

            return result;
        }

        public TDataEntity[] GetAll()
        {
            TDataEntity[] result = new TDataEntity[5];

            for (int i = 0; i < result.Length; i++)
            {
                TDataEntity entity = (TDataEntity)Activator.CreateInstance(typeof(TDataEntity));
                result[i] = entity;
            }

            this.getAllReturnValues.Add(result);

            return result;
        }

        public int GetCount()
        {
            int result = DateTime.Now.Second;

            this.getCountReturnValues.Add(result);

            return result;
        }

        public void RegisterNew(TDataEntity entity)
        {
            this.registerNewCalls.Add(entity);
        }

        public void RegisterDirty(TDataEntity entity)
        {
            this.registerDirtyCalls.Add(entity);
        }

        public void RegisterRemoved(TDataEntity entity)
        {
            this.registerRemovedCalls.Add(entity);
        }

        public void Commit()
        {
            this.OnCommitting();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }

        public void AssertGetByIdCalls(params TId[] calls)
        {
            CollectionAssert.AreEqual(this.getByIdCalls, calls);
        }

        public void AssertGetByIdReturnValues(params TDataEntity[] returnValues)
        {
            CollectionAssert.AreEqual(this.getByIdReturnValues, returnValues);
        }

        public void AssertGetAllReturnValues(params IEnumerable<TDataEntity>[] returnValues)
        {
            CollectionAssert.AreEqual(this.getAllReturnValues, returnValues);
        }

        public void AssertGetCountReturnValues(params int[] returnValues)
        {
            CollectionAssert.AreEqual(this.getCountReturnValues, returnValues);
        }

        public void AssertRegisterNewCalls(params TDataEntity[] calls)
        {
            CollectionAssert.AreEqual(this.registerNewCalls, calls);
        }

        public void AssertRegisterDirtyCalls(params TDataEntity[] calls)
        {
            CollectionAssert.AreEqual(this.registerDirtyCalls, calls);
        }

        public void AssertRegisterRemovedCalls(params TDataEntity[] calls)
        {
            CollectionAssert.AreEqual(this.registerRemovedCalls, calls);
        }

        public event EventHandler Committing;
        protected virtual void OnCommitting()
        {
            if (this.Committing != null)
            {
                this.Committing(this, EventArgs.Empty);
            }
        }
    }
}