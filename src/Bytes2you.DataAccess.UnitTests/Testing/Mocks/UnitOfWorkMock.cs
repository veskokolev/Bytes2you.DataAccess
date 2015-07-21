using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class UnitOfWorkMock : IUnitOfWork
    {
        private readonly List<object> getByIdCalls;
        private readonly List<object> getByIdReturnValues;
        private readonly List<object> getAllReturnValues;
        private readonly List<int> getCountReturnValues;
        private readonly List<object> registerNewCalls;
        private readonly List<object> registerDirtyCalls;
        private readonly List<object> registerRemovedCalls;

        public UnitOfWorkMock()
        {
            this.getByIdCalls = new List<object>();
            this.getByIdReturnValues = new List<object>();
            this.getAllReturnValues = new List<object>();
            this.getCountReturnValues = new List<int>();
            this.registerNewCalls = new List<object>();
            this.registerDirtyCalls = new List<object>();
            this.registerRemovedCalls = new List<object>();
        }

        public TDataEntity GetById<TDataEntity, TId>(TId id) where TDataEntity : class, IDataEntity<TId>
        {
            TDataEntity result = (TDataEntity)Activator.CreateInstance(typeof(TDataEntity));

            this.getByIdCalls.Add(id);
            this.getByIdReturnValues.Add(result);

            return result;
        }

        public IList<TDataEntity> GetAll<TDataEntity, TId>() where TDataEntity : class, IDataEntity<TId>
        {
            List<TDataEntity> result = new List<TDataEntity>();

            for (int i = 0; i < 5; i++)
            {
                TDataEntity entity = (TDataEntity)Activator.CreateInstance(typeof(TDataEntity));
                result.Add(entity);
            }

            this.getAllReturnValues.Add(result);

            return result;
        }

        public int GetCount<TDataEntity, TId>() where TDataEntity : class, IDataEntity<TId>
        {
            int result = DateTime.Now.Second;

            this.getCountReturnValues.Add(result);

            return result;
        }

        public void RegisterNew<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>
        {
            this.registerNewCalls.Add(entity);
        }

        public void RegisterDirty<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>
        {
            this.registerDirtyCalls.Add(entity);
        }

        public void RegisterRemoved<TDataEntity, TId>(TDataEntity entity) where TDataEntity : class, IDataEntity<TId>
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

        public void AssertGetByIdCalls(params object[] calls)
        {
            CollectionAssert.AreEqual(this.getByIdCalls, calls);
        }

        public void AssertGetByIdReturnValues(params object[] returnValues)
        {
            CollectionAssert.AreEqual(this.getByIdReturnValues, returnValues);
        }

        public void AssertGetAllReturnValues(params object[] returnValues)
        {
            CollectionAssert.AreEqual(this.getAllReturnValues, returnValues);
        }

        public void AssertGetCountReturnValues(params int[] returnValues)
        {
            CollectionAssert.AreEqual(this.getCountReturnValues, returnValues);
        }

        public void AssertRegisterNewCalls(params object[] calls)
        {
            CollectionAssert.AreEqual(this.registerNewCalls, calls);
        }

        public void AssertRegisterDirtyCalls(params object[] calls)
        {
            CollectionAssert.AreEqual(this.registerDirtyCalls, calls);
        }

        public void AssertRegisterRemovedCalls(params object[] calls)
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