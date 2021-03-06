﻿using System;
using System.Diagnostics;
using System.Linq;
using Bytes2you.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bytes2you.UnitTests.Core
{
    public static class Ensure
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

            Assert.IsTrue(watch.ElapsedMilliseconds <= 1.2 * expectedTimeInMilliseconds, assertionMessage);
        }

        public static void ActionRunsInExpectedTime(Action action, ExecutionTimeType executionTimeType)
        {
            int repeatCount;

            switch (executionTimeType)
            {
                case ExecutionTimeType.Normal:
                    repeatCount = 20;
                    break;

                case ExecutionTimeType.Fast:
                    repeatCount = 200;
                    break;

                case ExecutionTimeType.SuperFast:
                    repeatCount = 2000;
                    break;

                default:
                    throw new InvalidOperationException();
            }

            ActionRunsInExpectedTime(action, repeatCount, 1);
        }
    }
}