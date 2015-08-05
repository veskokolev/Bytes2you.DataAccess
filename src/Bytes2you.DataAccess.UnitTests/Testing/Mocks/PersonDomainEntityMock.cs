using System;
using System.Linq;
using Bytes2you.DataAccess.Domain;

namespace Bytes2you.DataAccess.UnitTests.Testing.Mocks
{
    public class PersonDomainEntityMock : IDomainEntity<int>
    {
        public int Id { get; set; }
    }
}