using System;
using System.Linq;
using Bytes2you.DataAccess.Data;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Mocks
{
    public class PersonDataEntityMock : IDataEntity<int>
    {
        public int Id { get; set; }
    }
}