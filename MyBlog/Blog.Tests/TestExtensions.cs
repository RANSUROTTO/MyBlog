using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Blog.Tests
{

    /// <summary>
    /// 对象断言扩展类
    /// </summary>
    public static class TestExtensions
    {

        #region Test null

        /// <summary>
        /// 断言对象为null
        /// </summary>
        /// <param name="obj">预期对象</param>
        public static T TestIsNull<T>(this T obj)
        {
            Assert.IsNull(obj);
            return obj;
        }

        /// <summary>
        /// 断言对象为null,不为null时显示错误信息
        /// </summary>
        /// <param name="obj">预期对象</param>
        /// <param name="message">错误消息</param>
        public static T TestIsNull<T>(this T obj, string message)
        {
            Assert.IsNull(obj, message);
            return obj;
        }

        /// <summary>
        /// 断言对象不为null
        /// </summary>
        /// <param name="obj">预期对象</param>
        public static T TestIsNotNull<T>(this T obj)
        {
            Assert.IsNotNull(obj);
            return obj;
        }

        /// <summary>
        /// 断言对象不为null,为null时显示错误信息
        /// </summary>
        /// <param name="obj">预期对象</param>
        /// <param name="message">错误消息</param>
        public static T TestIsNotNull<T>(this T obj, string message)
        {
            Assert.IsNotNull(obj, message);
            return obj;
        }

        #endregion

        #region Test equals

        /// <summary>
        /// 断言两个对象相等
        /// </summary>
        /// <param name="actual">实际对象</param>
        /// <param name="expected">预期对象</param>
        public static T TestEqual<T>(this T actual, object expected)
        {
            Assert.AreEqual(expected, actual);
            return actual;
        }

        ///<summary>
        /// 断言两个对象相等,不相等时显示错误消息
        ///</summary>
        ///<param name="actual">实际对象</param>
        ///<param name="expected">预期对象</param>
        ///<param name="message">错误消息</param>
        public static void TestEqual(this object actual, object expected, string message)
        {
            Assert.AreEqual(expected, actual, message, args: null);
        }

        #endregion

        #region Test Exception

        /// <summary>
        /// 断言一个测试函数会抛出指定类型异常
        /// </summary>
        /// <param name="exceptionType">预期异常类型</param>
        /// <param name="testDelegate">测试函数</param>
        /// <returns>异常对象</returns>
        public static Exception TestByThrownByMethod(this Type exceptionType, TestDelegate testDelegate)
        {
            return Assert.Throws(exceptionType, testDelegate);
        }

        #endregion

        #region Test Is Instance

        /// <summary>
        /// 断言预期对象是给定类型的实例
        /// </summary>
        /// <typeparam name="T">预期类型</typeparam>
        /// <param name="actual">预期对象</param>
        public static void TestBe<T>(this object actual)
        {
            Assert.IsInstanceOf<T>(actual);
        }

        #endregion

        #region Test Same as 

        /// <summary>
        /// 断言两个对象引用相同
        /// </summary>
        /// <param name="actual">实际对象</param>
        /// <param name="expected">预期对象</param>
        public static void ShouldBeTheSameAs(this object actual, object expected)
        {
            Assert.AreSame(expected, actual);
        }

        /// <summary>
        /// 断言两个对象引用不相同
        /// </summary>
        /// <param name="actual">实际对象</param>
        /// <param name="expected">预期对象</param>
        public static void ShouldBeNotBeTheSameAs(this object actual, object expected)
        {
            Assert.AreNotSame(expected, actual);
        }

        #endregion

        #region Test Boolean

        /// <summary>
        /// 断言条件为true
        /// </summary>
        public static void ShouldBeTrue(this bool source)
        {
            Assert.IsTrue(source);
        }

        /// <summary>
        /// 断言条件为false
        /// </summary>
        /// <param name="source"></param>
        public static void ShouldBeFalse(this bool source)
        {
            Assert.IsFalse(source);
        }

        #endregion

        #region Test string

        /// <summary>
        /// 断言两个字符串相等(不区分大小写)
        /// </summary>
        /// <param name="actual">实际字符串</param>
        /// <param name="expected">预期字符串</param>
        public static void AssertSameStringAs(this string actual, string expected)
        {
            Assert.IsTrue(string.Equals(actual, expected, StringComparison.InvariantCultureIgnoreCase));
        }

        #endregion

        /* public static T PropertiesShouldEqual<T>(this T actual, T expected, params string[] filters)
         {
             var properties = typeof(T).GetProperties().ToList();

             var filterByEnteties = new List<string>();
             var values = new Dictionary<string, object>();

             foreach (var propertyInfo in properties.ToList())
             {
                 //skip by filter
                 if (filters.Any(f => f == propertyInfo.Name || f + "Id" == propertyInfo.Name) || propertyInfo.Name == "Id")
                     continue;
                 var value = propertyInfo.GetValue(actual);
                 values.Add(propertyInfo.Name, value);

                 if (value == null)
                     continue;

                 //skip array and System.Collections.Generic types
                 if (value.GetType().IsArray || value.GetType().Namespace == "System.Collections.Generic")
                 {
                     properties.Remove(propertyInfo);
                     continue;
                 }

                 if (!(value is BaseEntity))
                     continue;

                 //skip BaseEntity types and enteties Id
                 filterByEnteties.Add(propertyInfo.Name + "Id");
                 properties.Remove(propertyInfo);
             }

             foreach (var propertyInfo in properties.Where(p => values.ContainsKey(p.Name)))
             {
                 if (filterByEnteties.Any(f => f == propertyInfo.Name))
                     continue;

                 Assert.AreEqual(values[propertyInfo.Name], propertyInfo.GetValue(expected), string.Format("The property \"{0}.{1}\" of these objects is not equal", typeof(T).Name, propertyInfo.Name));
             }
             return actual;
         }*/

        #region Utilities

        /// <summary>
        /// 尝试将对象转换为指定类型
        /// </summary>
        /// <typeparam name="T">转换类型</typeparam>
        /// <param name="source">实际对象</param>
        /// <returns>T对象实例</returns>
        public static T CastTo<T>(this object source)
        {
            return (T)source;
        }

        #endregion

    }
}
