using System;

namespace Blog.Libraries.Core.Caching
{

    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager : IDisposable
    {

        /// <summary>
        /// 获取或设置与指定键相关联的值。
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        T Get<T>(string key);

        /// <summary>
        /// 将指定的键和对象添加到缓存。
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">value</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 获取一个值，指示与指定键相关联的值是否被缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>结果</returns>
        bool Any(string key);

        /// <summary>
        /// 从缓存中删除指定键的值
        /// </summary>
        /// <param name="key">key</param>
        void Remove(string key);

        /// <summary>
        /// 按指定的模式删除缓存
        /// </summary>
        /// <param name="pattern">模式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清空所有缓存数据
        /// </summary>
        void Clear();

    }
}
