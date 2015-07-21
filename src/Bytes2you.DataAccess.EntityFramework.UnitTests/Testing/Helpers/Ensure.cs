using System;
using System.Diagnostics;
using System.Linq;
using Bytes2you.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.DataAccess.EntityFramework.UnitTests.Testing.Helpers
{
    internal static class Ensure
    {
        public static void ArgumentExceptionIsThrown(Action action, string expectedArgumentName, string expectedMessage = null)
        {
            Guard.WhenArgument(action, "action").IsNull().Throw();
            Guard.WhenArgument(expectedArgumentName, "expectedArgumentName").IsNullOrEmpty().Throw();

            ArgumentException ex = null;
            try
            {
                action();
            }
            catch (ArgumentException e)
            {
                ex = e;
            }

            Assert.AreEqual(typeof(ArgumentException), ex.GetType());
            Assert.IsNotNull(ex);
            Assert.AreEqual(expectedArgumentName, ex.ParamName);

            if (expectedMessage != null)
            {
                Assert.AreEqual(ex.Message, expectedMessage);
            }
        }

        public static void ArgumentNullExceptionIsThrown(Action action, string expectedArgumentName)
        {
            Guard.WhenArgument(action, "action").IsNull().Throw();
            Guard.WhenArgument(expectedArgumentName, "expectedArgumentName").IsNullOrEmpty().Throw();

            ArgumentNullException ex = null;
            try
            {
                action();
            }
            catch (ArgumentNullException e)
            {
                ex = e;
            }

            Assert.AreEqual(typeof(ArgumentNullException), ex.GetType());
            Assert.IsNotNull(ex);
            Assert.AreEqual(expectedArgumentName, ex.ParamName);
        }

        public static void ArgumentOutOfRangeExceptionIsThrown(Action action, string expectedArgumentName)
        {
            Guard.WhenArgument(action, "action").IsNull().Throw();
            Guard.WhenArgument(expectedArgumentName, "expectedArgumentName").IsNullOrEmpty().Throw();

            ArgumentOutOfRangeException ex = null;
            try
            {
                action();
            }
            catch (ArgumentOutOfRangeException e)
            {
                ex = e;
            }

            Assert.AreEqual(typeof(ArgumentOutOfRangeException), ex.GetType());
            Assert.IsNotNull(ex);
            Assert.AreEqual(expectedArgumentName, ex.ParamName);
        }

        public static void NoExceptionIsThrown(Action action)
        {
            Guard.WhenArgument(action, "action").IsNull().Throw();
            
            try
            {
                action();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        public static void ActionRunsInExpectedTime(Action action, int repeatCount, int expectedTimeInMilliseconds)
        {
            Guard.WhenArgument(action, "action").IsNull().Throw();
            Guard.WhenArgument(repeatCount, "repeatCount").IsLessThanOrEqual(0).Throw();
            Guard.WhenArgument(expectedTimeInMilliseconds, "expectedTimeInMilliseconds").IsLessThanOrEqual(0).Throw();

            Stopwatch watch = new Stopwatch();

            for (int i = 0; i < repeatCount; i++)
            {
                watch.Start();

                action();

                watch.Stop();
            }

            string assertionMessage = string.Format(
                "The given action does not perform as expected. repeatCount: {0}; expectedTimeInMilliseconds: {1}; actualTimeInMilliseconds: {2}", 
                repeatCount, 
                expectedTimeInMilliseconds, 
                watch.ElapsedMilliseconds);

            Assert.IsTrue(watch.ElapsedMilliseconds < expectedTimeInMilliseconds, assertionMessage);
        }
    }
}