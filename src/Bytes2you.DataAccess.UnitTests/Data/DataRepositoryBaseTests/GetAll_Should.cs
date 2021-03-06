﻿using System;
using System.Linq;
using Bytes2you.DataAccess.UnitTests.Testing.Mocks;
using Bytes2you.UnitTests.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Data.DataRepositoryBaseTests
{
    [TestClass]
    public class GetAll_Should : DataRepositoryBaseTestBase
    {
        [TestMethod]
        public void CallUnitOfWorkGetAllMethodAndReturnItsResult()
        {
            // Act.
            PersonDataEntityMock[] returnValue = this.DataRepository.GetAll();
            
            // Assert.
            this.UnitOfWorkMock.AssertGetAllReturnValues(returnValue);
        }

        [TestMethod]
        public void RunInExpectedTime()
        {
            // Act & Assert.
            Ensure.ActionRunsInExpectedTime(
                () =>
                {
                    this.DataRepository.GetAll();
                },
                ExecutionTimeType.Fast);
        }
    }
}