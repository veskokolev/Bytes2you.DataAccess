using System;
using System.Linq;
using Bytes2you.Validation;

namespace Bytes2you.DataAccess.UnitTests.Testing.Helpers
{
    internal static class TupleHelper
    {
        public static Tuple<T1, T2>[] CombineIntoTupleArray<T1, T2>(T1[] t1, T2[] t2)
        {
            Guard.WhenArgument(t1, "t1").IsNull().Throw();
            Guard.WhenArgument(t2, "t2").IsNull().Throw();
            Guard.WhenArgument(t1.Length, "t1.Length").IsNotEqual(t2.Length).Throw();

            Tuple<T1, T2>[] tuples = new Tuple<T1, T2>[t1.Length];
            for (int i = 0; i < t1.Length; i++)
            {
                tuples[i] = new Tuple<T1, T2>(t1[i], t2[i]);
            }

            return tuples;
        }
    }
}
