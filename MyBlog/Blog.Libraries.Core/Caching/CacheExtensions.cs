using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Blog.Libraries.Core.Caching
{

    /// <summary>
    /// 缓存管理类扩展
    /// </summary>
    public static class CacheExtensions
    {

        /// <summary>
        /// 获取缓存对象. 如果尚未缓存，则加载并缓存它
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheManager">缓存管理对象</param>
        /// <param name="key">键</param>
        /// <param name="acquire">如果不在缓存中，则加载方法取得返回值进行缓存</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, Func<T> acquire)
        {
            return Get(cacheManager, key, 60, acquire);
        }

        /// <summary>
        /// 获取缓存对象. 如果尚未缓存，则加载并缓存它
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="cacheManager">缓存管理对象</param>
        /// <param name="key">键</param>
        /// <param name="cacheTime">缓存时间 / min</param>
        /// <param name="acquire">如果不在缓存中，则加载方法取得返回值进行缓存</param>
        /// <returns>缓存对象</returns>
        public static T Get<T>(this ICacheManager cacheManager, string key, int cacheTime, Func<T> acquire)
        {
            if (cacheManager.Any(key))
            {
                return cacheManager.Get<T>(key);
            }

            var result = acquire();
            if (cacheTime > 0)
                cacheManager.Set(key, result, cacheTime);
            return result;
        }

        /// <summary>
        /// 按指定模式删除缓存项
        /// </summary>
        /// <param name="cacheManager">缓存对象</param>
        /// <param name="pattern">模式</param>
        /// <param name="keys">缓存所有键集合</param>
        public static void RemoveByPattern(this ICacheManager cacheManager, string pattern, IEnumerable<string> keys)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //如果键值 匹配 正则表达式，则删除该缓存项
            foreach (var key in keys.Where(p => regex.IsMatch(p.ToString())).ToList())
                cacheManager.Remove(key);
        }

    }
}
