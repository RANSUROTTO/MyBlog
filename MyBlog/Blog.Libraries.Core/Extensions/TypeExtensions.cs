using System;

namespace Blog.Libraries.Core.Extensions
{
    public static class TypeExtensions
    {

        /// <summary>
        /// 获得一个值,表示类型是否为基础类型或string类型.
        /// </summary>
        public static bool IsBasicTypeOrString(this Type type)
        {
            if (type == typeof(string))
                return true;
            return type.IsPrimitive;
        }

    }
}
