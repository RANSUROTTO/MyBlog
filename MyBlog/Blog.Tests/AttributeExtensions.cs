using System;
using System.Reflection;
using NUnit.Framework;

namespace Blog.Tests
{

    /// <summary>
    /// 自定义特性的扩展类
    /// </summary>
    public static class AttributeExtensions
    {

        /// <summary>
        /// 如果attributeTarget对象使用了TAttribute类进行修饰,则返回true
        /// 没有则返回false
        /// </summary>
        /// <typeparam name="TAttribute">预期修饰的Attribute类</typeparam>
        /// <param name="attributeTarget">对象</param>
        public static bool IsDecoratedWith<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return attributeTarget.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        }

        /// <summary>
        /// 返回修饰attributeTarget对象所用的TAttribute类对象
        /// </summary>
        /// <typeparam name="TAttribute">预期修饰的Attribute类</typeparam>
        /// <param name="attributeTarget">对象</param>
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return (TAttribute)attributeTarget.GetCustomAttributes(typeof(TAttribute), false)[0];
        }

    }

    /// <summary>
    /// 针对自定义特性的扩展类的单元测试
    /// </summary>
    [TestFixture]
    public class AttributeExtensionsTests
    {

        #region Attribute and Model

        public class AttrTestAttribute : Attribute { }
        public class FailAttrTestAttribute : Attribute { }

        public class AttrTestModel
        {
            [AttrTest]
            public string Name { get; set; }
        }

        #endregion

        #region UnitTest AttributeExtensions.IsDecoratedWith_Generice

        [Test]
        public void PassesOnAttributeAny_generice()
        {
            Type classType = typeof(AttrTestModel);
            PropertyInfo property = classType.GetProperty("Name");
            bool flag = property.IsDecoratedWith<AttrTestAttribute>();
            Assert.AreEqual(true, flag);
        }

        [Test]
        [Ignore("这是一个单元测试结果为失败的方法")]
        public void ReturnTheAttributeEquals_generice_fail()
        {
            Type classType = typeof(AttrTestModel);
            PropertyInfo property = classType.GetProperty("Name");
            bool flag = property.IsDecoratedWith<FailAttrTestAttribute>();
            Assert.AreEqual(true, flag);
        }

        #endregion

        #region UnitTest AttributeExtensions.GetAttribute_Generice

        [Test]
        public void PassesOnAttributeGet_generice()
        {
            Type classType = typeof(AttrTestModel);
            PropertyInfo property = classType.GetProperty("Name");
            var result = property.GetAttribute<AttrTestAttribute>();

            TypeAssert.AreEqual(typeof(AttrTestAttribute), result);
        }

        #endregion

    }

}
