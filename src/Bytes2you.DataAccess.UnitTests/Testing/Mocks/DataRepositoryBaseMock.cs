using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class DataRepositoryBaseMock<TDataEntity> : DataRepositoryBase<TDataEntity, int, UnitOfWorkMock>
        where TDataEntity : class, IDataEntity<int>, new()
    {
        private readonly List<int> deleteEntityIdCalls;

        public DataRepositoryBaseMock(UnitOfWorkMock unitOfWorkMock)
            : base(unitOfWorkMock)
        {
            this.deleteEntityIdCalls = new List<int>();
        }

        public void AssertUnitOfWorkProperty(IUnitOfWork unitOfWork)
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