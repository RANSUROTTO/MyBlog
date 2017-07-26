using System;
using NUnit.Framework;

namespace Blog.Tests
{

    /// <summary>
    /// 包含用于单元测试的类型断言
    /// </summary>
    public static class TypeAssert
    {

        /// <summary>
        /// 检查两个对象类型是否相等
        /// </summary>
        /// <param name="expected">预期类型对象</param>
        /// <param name="instance">实例类型对象</param>
        public static void AreEqual(object expected, object instance)
        {
            if (expected == null)
                Assert.IsNull(instance);
            else
                Assert.IsNotNull(instance, "Instance was null");
            Assert.AreEqual(expected.GetType(), instance.GetType(), "Expected: " + expected.GetType() + ", was: " + instance.GetType() + " was not of type " + instance.GetType());
        }

        /// <summary>
        /// 检查对象是否匹配预期类型
        /// </summary>
        /// <param name="expected">预期类型</param>
        /// <param name="instance">实例类型对象</param>
        public static void AreEqual(Type expected, object instance)
        {
            if (expected == null)
                Assert.IsNull(instance);
            else
                Assert.IsNotNull(instance, "Instance was null");
            Assert.AreEqual(expected, instance.GetType(), "Expected: " + expected + ", was: " + instance.GetType() + " was not of type " + instance.GetType());
        }

        /// <summary>
        /// 检查对象是否匹配泛型预期类型T
        /// </summary>
        /// <typeparam name="T">预期类型</typeparam>
        /// <param name="instance">实例类型对象</param>
        public static void Equals<T>(object instance)
        {
            AreEqual(typeof(T), instance);
        }

        /// <summary>
        /// 检查实例类型是否派生于泛型预期类型T
        /// </summary>
        /// <typeparam name="T">预期类型</typeparam>
        /// <param name="instance">实例类型对象</param>
        public static void Is<T>(object instance)
        {
            Assert.IsTrue(instance is T, "Instance " + instance + " was not of type " + typeof(T));
        }

    }

    /// <summary>
    /// 针对类型断言类(TypeAssert)的单元测试
    /// </summary>
    [TestFixture]
    public class TypeAssertTests
    {

        #region UnitTest TypeAssert.AreEqual

        [Test]
        public void PassesOnTypeEqualsByObject()
        {
            string str1 = "123";
            string str2 = "abc";
            TypeAssert.AreEqual(str1, str2);
        }

        [Test]
        public void PassesOnTypeEqualsByType()
        {
            string str = "abc";
            TypeAssert.AreEqual(typeof(string), str);
        }

        #endregion

        #region UnitTest TypeAssert.Equals_Generice

        [Test]
        public void PassesOnTypeEquals_generice()
        {
            string str = "abc";
            TypeAssert.Equals<string>(str);
        }

        #endregion

        #region UnitTest TypeAssert.Is_Generice

        [Test]
        public void PassesTypeIs_generice()
        {
            string str = "abc";
            TypeAssert.Is<object>(str);
        }

        #endregion

        [Test]
        public void PassesStringIsNotPrimitive()
        {
            string str = "";
            str.GetType().IsPrimitive.TestBeFalse();
        }

        [Test]
        public void PassesStringEmptySplitNotTriggerException()
        {
            try
            {
                string str = string.Empty;
                string[] strs = str.Split(';');
                strs.TestIsNotNull();
            }
            catch (Exception ex)
            {
                Assert.Fail("method throw exception,exceptionMessage:{0}", ex.Message);
            }
        }

    }

}
