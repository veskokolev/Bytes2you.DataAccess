using System;
using System.Linq;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class PersonDataEntityMock : IDataEntity<int>
    {
        public int Id { get; set; }
    }
}