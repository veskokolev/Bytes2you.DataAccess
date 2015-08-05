using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Bytes2you.DataAccess.UnitTests.Testing.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class DataRepositoryBaseMock<TDataEntity> : DataRepositoryBase<TDataEntity, int>
        where TDataEntity : class, IDataEntity<int>, new()
    {
        private readonly List<int> getByIdCalls;
        private readonly List<int> deleteWithEntityIdCalls;
        private int getAllCallCount;
        private int getCountCallCount;
        private readonly List<TDataEntity> insertCalls;
        private readonly List<IEnumerable<TDataEntity>> insertAllCalls;
        private readonly List<TDataEntity> updateCalls;
        private readonly List<IEnumerable<TDataEntity>> updateAllCalls;
        private readonly List<TDataEntity> saveCalls;
        private readonly List<IEnumerable<TDataEntity>> saveAllCalls;
        private readonly List<int> deleteWithIdCalls;
        private readonly List<IEnumerable<TDataEntity>> deleteAllCalls;

        public DataRepositoryBaseMock(IUnitOfWork<TDataEntity, int> unitOfWork)
            : base(unitOfWork)
        {
            this.getByIdCalls = new List<int>();
            this.insertCalls = new List<TDataEntity>();
            this.insertAllCalls = new List<IEnumerable<TDataEntity>>();
            this.updateCalls = new List<TDataEntity>();
            this.updateAllCalls = new List<IEnumerable<TDataEntity>>();
            this.saveCalls = new List<TDataEntity>();
            this.saveAllCalls = new List<IEnumerable<TDataEntity>>();
            this.deleteWithIdCalls = new List<int>();
            this.deleteWithEntityIdCalls = new List<int>();
            this.deleteAllCalls = new List<IEnumerable<TDataEntity>>();
        }

        public void AssertUnitOfWorkProperty(IUnitOfWork<TDataEntity, int> unitOfWork)
        {
            Assert.AreSame(unitOfWork, this.UnitOfWork);
        }

        public override TDataEntity GetById(int id)
        {
            this.getByIdCalls.Add(id);
            return base.GetById(id);
        }

        public override TDataEntity[] GetAll()
        {
            this.getAllCallCount++;
            return base.GetAll();
        }

        public override int GetCount()
        {
            this.getCountCallCount++;
            return base.GetCount();
        }

        public override TDataEntity Insert(TDataEntity entity)
        {
            this.insertCalls.Add(entity);
            return base.Insert(entity);
        }

        public override TDataEntity[] InsertAll(IEnumerable<TDataEntity> entities)
        {
            this.insertAllCalls.Add(entities);
            return base.InsertAll(entities);
        }

        public override TDataEntity Update(TDataEntity entity)
        {
            this.updateCalls.Add(entity);
            return base.Update(entity);
        }

        public override TDataEntity[] UpdateAll(IEnumerable<TDataEntity> entities)
        {
            this.updateAllCalls.Add(entities);
            return base.UpdateAll(entities);
        }

        public override TDataEntity Save(TDataEntity entity)
        {
            this.saveCalls.Add(entity);
            return base.Save(entity);
        }

        public override TDataEntity[] SaveAll(IEnumerable<TDataEntity> entities)
        {
            this.saveAllCalls.Add(entities);
            return base.SaveAll(entities);
        }

        public override void Delete(int id)
        {
            this.deleteWithIdCalls.Add(id);
            base.Delete(id);
        }

        public override void Delete(TDataEntity entity)
        {
            base.Delete(entity);
            this.deleteWithEntityIdCalls.Add(entity.Id);
        }

        public override void DeleteAll(IEnumerable<TDataEntity> entities)
        {
            this.deleteAllCalls.Add(entities);
            base.DeleteAll(entities);
        }

        public void AssertGetByIdCalls(params int[] callIds)
        {
            CollectionAssert.AreEqual(this.getByIdCalls, callIds);
        }

        public void AssertGetAllCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.getAllCallCount);
        }

        public void AssertGetCountCallCount(int callCount)
        {
            Assert.AreEqual(callCount, this.getCountCallCount);
        }

        public void AssertInsertCalls(params TDataEntity[] callEntities)
        {
            CollectionAssert.AreEqual(this.insertCalls, callEntities);
        }

        public void AssertInsertAllCalls(params IEnumerable<TDataEntity>[] callEntities)
        {
            CollectionAsserterEx.AreSequenceEqual(this.insertAllCalls, callEntities);
        }

        public void AssertUpdateCalls(params TDataEntity[] callEntities)
        {
            CollectionAssert.AreEqual(this.updateCalls, callEntities);
        }

        public void AssertUpdateAllCalls(params IEnumerable<TDataEntity>[] callEntities)
        {
            CollectionAsserterEx.AreSequenceEqual(this.updateAllCalls, callEntities);
        }

        public void AssertSaveCalls(params TDataEntity[] callEntities)
        {
            CollectionAssert.AreEqual(this.saveCalls, callEntities);
        }

        public void AssertSaveAllCalls(params IEnumerable<TDataEntity>[] callEntities)
        {
            CollectionAsserterEx.AreSequenceEqual(this.saveAllCalls, callEntities);
        }

        public void AssertDeleteWithIdCalls(params int[] callIds)
        {
            CollectionAssert.AreEqual(this.deleteWithIdCalls, callIds);
        }

        public void AssertDeleteWithEntityCalls(params int[] callIds)
        {
            CollectionAssert.AreEqual(this.deleteWithEntityIdCalls, callIds);
        }

        public void AssertDeleteAllCalls(params IEnumerable<TDataEntity>[] callEntities)
        {
            CollectionAsserterEx.AreSequenceEqual(this.deleteAllCalls, callEntities);
        }
    }
}