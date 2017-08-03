using System;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;
using Blog.Libraries.Core.Infrastructure;

namespace Blog.Libraries.Core.Caching.RedisCaching
{

    /// <summary>
    /// Redis缓存管理
    /// </summary>
    public class RedisCacheManager : ICacheManager
    {

        #region Fields

        private readonly IRedisConnectionWrapper _connectionWrapper;
        private readonly IDatabase _db;
        private readonly ICacheManager _perRequestCacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="connectionWrapper">已初始化的redis连接管理器</param>
        public RedisCacheManager(IRedisConnectionWrapper connectionWrapper)
        {
            if (connectionWrapper == null)
                throw new ArgumentNullException("connectionWrapper");

            this._connectionWrapper = connectionWrapper;
            this._db = _connectionWrapper.GetDatabase();

            //Unit tests need to annot the following code
            this._perRequestCacheManager = EngineContext.Current.Resolve<ICacheManager>();
        }

        #endregion

        #region Methods

        public virtual T Get<T>(string key)
        {
            if (_perRequestCacheManager?.HasKey(key) ?? false)
                return _perRequestCacheManager.Get<T>(key);

            var rValue = _db.StringGet(key);
            if (!rValue.HasValue)
                return default(T);
            var result = Deserialize<T>(rValue);

            _perRequestCacheManager?.Set(key, result, 0);
            return result;
        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            if (data == null)
                return;

            var entryBytes = Serialize(data);
            var expiresIn = TimeSpan.FromMinutes(cacheTime);

            _db.StringSet(key, entryBytes, expiresIn);
        }

        public virtual bool HasKey(string key)
        {
            return _db.KeyExists(key);
        }

        public virtual void Remove(string key)
        {
            _db.KeyDelete(key);
            _perRequestCacheManager?.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {
            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);
                var keys = server.Keys(database: _db.Database, pattern: "*" + pattern + "*");
                foreach (var key in keys)
                    Remove(key);
            }
        }

        public virtual void Clear()
        {
            foreach (var ep in _connectionWrapper.GetEndPoints())
            {
                var server = _connectionWrapper.GetServer(ep);

                //可以取消注释下面的代码
                //server.FlushDatabase();
                //但是它们需要有管理权限,allowAdmin=true
                var keys = server.Keys(database: _db.Database);
                foreach (var key in keys)
                    Remove(key);
            }
        }

        public virtual void Dispose()
        {
            _connectionWrapper?.Dispose();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 将对象序列为json字符串的二进制数组
        /// </summary>
        protected virtual byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        /// <summary>
        /// 将二进制数组转为json字符串反序列为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializedObject"></param>
        /// <returns></returns>
        protected virtual T Deserialize<T>(byte[] serializedObject)
        {
            if (serializedObject == null)
                return default(T);

            var jsonString = Encoding.UTF8.GetString(serializedObject);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        #endregion

    }

}
