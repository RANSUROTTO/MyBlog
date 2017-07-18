using System;
using System.Reflection;
using NUnit.Framework;

namespace Blog.Tests
{
    /// <summary>
    /// 包含用于单元测试的异常断言
    /// </summary>
    public class ExceptionAssert
    {

        /// <summary>
        /// 执行一个可能抛出异常的方法,检查抛出异常的异常是否匹配预期异常的类型
        /// </summary>
        /// <param name="exceptionType">期望的异常类型</param>
        /// <param name="method">执行的方法</param>
        /// <returns>实际抛出的异常</returns>
        public static Exception Throws(Type exceptionType, Action method)
        {
            try
            {
                method.Invoke();
            }
            catch (Exception ex)
            {
                Assert.AreEqual(exceptionType, ex.GetType());
                return ex;
            }
            Assert.Fail("Expected exception '" + exceptionType.FullName + "' wasn't thrown.");
            return null;
        }

        /// <summary>
        /// 执行一个可能抛出异常的方法,检查抛出异常的异常是否匹配预期异常的泛型类型T
        /// </summary>
        /// <typeparam name="T">期望的异常类型</typeparam>
        /// <param name="method">执行的方法</param>
        /// <returns>实际抛出的异常</returns>
        public static T Throws<T>(Action method)
            where T : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (TargetInvocationException ex)
            {
                Assert.That(ex.InnerException, Is.TypeOf(typeof(T)));
            }
            catch (T ex)
            {
                return ex;
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected exception '" + typeof(T).FullName + "' but got exception '" + ex.GetType() + "'.");
                return null;
            }
            Assert.Fail("Expected exception '" + typeof(T).FullName + "' wasn't thrown.");
            return null;
        }

        /// <summary>
        /// 执行一个可能抛出异常的方法,检查抛出异常的异常是否匹配预期异常的泛型类型T
        /// </summary>
        /// <typeparam name="T">期望的异常类型</typeparam>
        /// <param name="method">执行的方法</param>
        public static void InnerException<T>(Action method)
            where T : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (Exception ex)
            {
                TypeAssert.AreEqual(typeof(T), ex.InnerException);
                return;
            }
            Assert.Fail("Expected exception '" + typeof(T).FullName + "' wasn't thrown.");
        }

    }


    /// <summary>
    /// 针对异常断言类(ExceptionAssert)的单元测试
    /// </summary>
    [TestFixture]
    public class ExceptionAssertTests
    {

    }


}
