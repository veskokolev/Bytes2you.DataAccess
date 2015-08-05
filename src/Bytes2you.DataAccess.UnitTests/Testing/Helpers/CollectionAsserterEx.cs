using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.UnitTests.Testing.Helpers
{
    internal static class CollectionAsserterEx
    {
        public static void AreSequenceEqual<T>(ICollection<IEnumerable<T>> expected, ICollection<IEnumerable<T>> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                CollectionAssert.AreEquivalent(expected.ElementAt(i).ToArray(), actual.ElementAt(i).ToArray());
            }
        }
    }
}