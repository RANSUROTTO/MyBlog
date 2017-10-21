using System;
using System.Linq.Expressions;
using System.Reflection;
using Blog.Libraries.Core.Configuration;

namespace Blog.Libraries.Services.Configuration
{
    public static class SettingExtenisons
    {

        /// <summary>
        /// 获取设定键值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <typeparam name="TPropType">类型</typeparam>
        /// <param name="entity">对象</param>
        /// <param name="keySelector">属性</param>
        /// <returns>键</returns>
        public static string GetSettingKey<T, TPropType>(this T entity,
            Expression<Func<T, TPropType>> keySelector)
            where T : ISettings, new()
        {
            var member = keySelector.Body as MemberExpression;
            if (member == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    keySelector));
            }

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    keySelector));
            }

            var key = typeof(T).Name + "." + propInfo.Name;
            return key;
        }

    }
}
