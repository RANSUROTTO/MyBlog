using System;
using System.Linq;
using System.Runtime.Caching;

namespace Blog.Libraries.Core.Caching
{

    /// <summary>
    /// 代表HTTP请求之间的缓存管理器（长期缓存）
    /// </summary>
    public class MemoryCacheManager : ICacheManager
    {

        #region Properties

        /// <summary>
        /// 缓存对象
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        #endregion

        #region Methods

        public virtual T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);
            Cache.Add(new CacheItem(key, data), policy);
        }

        public virtual bool Any(string key)
        {
            return Cache.Contains(key);
        }

        public virtual void Remove(string key)
        {
            Cache.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, Cache.Select(p => p.Key));
        }

        public virtual void Clear()
        {
            foreach (var item in Cache)
                Remove(item.Key);
        }

        public virtual void Dispose()
        { }

        #endregion

    }
}
