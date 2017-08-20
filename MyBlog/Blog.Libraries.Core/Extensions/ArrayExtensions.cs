using System.Collections.Generic;

namespace Blog.Libraries.Core.Extensions
{

    public static class ArrayExtensions
    {

        /// <summary>
        /// 将集合内的所有项转换为字符串按指定字符串拼接在一起
        /// </summary>
        /// <param name="list">集合</param>
        /// <param name="separator">分割字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string Join<T>(this IEnumerable<T> list, string separator = ",")
        {
            return string.Join(separator, list);
        }

    }

}
