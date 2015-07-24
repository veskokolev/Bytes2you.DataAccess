using System;
using System.Collections.Generic;
using System.Linq;
using Bytes2you.DataAccess.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class DataRepositoryBaseMock<TDataEntity> : DataRepositoryBase<TDataEntity, int>
        where TDataEntity : class, IDataEntity<int>, new()
    {
        private readonly List<int> deleteEntityIdCalls;

        public DataRepositoryBaseMock(IUnitOfWork<TDataEntity, int> unitOfWork)
            : base(unitOfWork)
        {
            this.deleteEntityIdCalls = new List<int>();
        }

        public void AssertUnitOfWorkProperty(IUnitOfWork<TDataEntity, int> unitOfWork)
        {
            Assert.AreSame(unitOfWork, this.UnitOfWork);
        }

        public override void Delete(TDataEntity entity)
        {
            base.Delete(entity);
            this.deleteEntityIdCalls.Add(entity.Id);
        }

        public void AssertDeleteEntityCalls(params int[] callIds)
        {
            CollectionAssert.AreEqual(this.deleteEntityIdCalls, callIds);
        }
    }
}