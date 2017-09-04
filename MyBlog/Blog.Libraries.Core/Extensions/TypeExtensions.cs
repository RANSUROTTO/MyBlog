using System;

namespace Blog.Libraries.Core.Extensions
{

    /// <summary>
    /// 针对类型的扩展类
    /// </summary>
    public static class TypeExtensions
    {

        /// <summary>
        /// 获得一个值,表示类型是否为基础类型或其它基本类型(string or datetime).
        /// </summary>
        public static bool IsCSharpBasicTypeOrOtherBasicType(this Type type)
        {
            if (type == typeof(string) || type == typeof(DateTime))
                return true;
            return type.IsPrimitive;
        }

    }
}
