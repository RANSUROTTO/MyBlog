using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;

namespace Blog.Libraries.Core.Caching.MemCaching
{
    public class MemcachedManager : ICacheManager
    {

        #region Field

        private volatile MemcachedClient _client;

        private readonly object _lock = new object();

        private readonly Lazy<string> _sectionName;

        #endregion

        #region Properties

        protected string SectionName { get { return _sectionName.Value; } }

        #endregion

        #region Constructor

        public MemcachedManager(string sectionName)
        {
            _sectionName = new Lazy<string>(() => sectionName);

        }

        #endregion

        #region Methods

        public T Get<T>(string key)
        {
            return GetMemcachedClient().Get<T>(key);
        }

        public void Set(string key, object data, int cacheTime)
        {
            GetMemcachedClient().Store(StoreMode.Set, key, data, TimeSpan.FromMinutes(cacheTime));
        }

        public bool HasKey(string key)
        {
            return GetMemcachedClient().Get(key) != null;
        }

        public void Remove(string key)
        {
            GetMemcachedClient().Remove(key);
        }

        /// <summary>
        /// Not Implemented
        /// </summary>
        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            GetMemcachedClient().FlushAll();
        }

        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 获得Memcached操作客户端
        /// </summary>
        protected MemcachedClient GetMemcachedClient()
        {
            if (_client != null) return _client;

            lock (_lock)
            {
                if (_client != null) return _client;
                //创建新的Memcached操作客户端
                _client = new MemcachedClient(_sectionName.Value);
            }
            return _client;
        }

        #endregion

    }
}
