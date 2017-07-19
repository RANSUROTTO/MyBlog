using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Blog.Tests
{

    public static class AttributeExtensions
    {

        /// <summary>
        /// 如果属性attributeTarget使用了TAttribute类进行修饰,则返回true
        /// 没有则返回false
        /// </summary>
        /// <typeparam name="TAttribute">预期修饰的Attribute类</typeparam>
        /// <param name="attributeTarget">属性</param>
        /// <returns></returns>
        public static bool IsDecoratedWith<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return attributeTarget.GetCustomAttributes(typeof(TAttribute), false).Length > 0;
        }

        /// <summary>
        /// Will return true the first attribute of type TAttribute on the attributeTarget.
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="attributeTarget"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this ICustomAttributeProvider attributeTarget) where TAttribute : Attribute
        {
            return (TAttribute)attributeTarget.GetCustomAttributes(typeof(TAttribute), false)[0];
        }

    }

}
