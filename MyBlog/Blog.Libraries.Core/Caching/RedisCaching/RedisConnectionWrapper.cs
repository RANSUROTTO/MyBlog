using System;
using System.Linq;
using System.Net;
using Blog.Libraries.Core.Configuration;
using RedLock;
using StackExchange.Redis;

namespace Blog.Libraries.Core.Caching.RedisCaching
{

    /// <summary>
    /// Redis连接包装实现类
    /// 它应该被注册为单例的生命周期形式
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {

        #region Ctor

        public RedisConnectionWrapper(WebConfig webconfig)
        {
            this._connectionString = new Lazy<string>(() => webconfig.RedisCachingConfig);
            this._redisLockFactory = CreateRedisLockFactory();
        }

        #endregion

        #region Field

        private readonly Lazy<string> _connectionString;

        private volatile ConnectionMultiplexer _connection;
        private volatile RedisLockFactory _redisLockFactory;
        private readonly object _lock = new object();

        #endregion

        #region Properties

        /// <summary>
        /// 获得连接字符串
        /// </summary>
        protected string ConnectionString { get { return _connectionString.Value; } }

        #endregion

        #region Methods

        public IDatabase GetDatabase(int? db = null)
        {
            return GetConnection().GetDatabase(db ?? -1);
        }

        public IServer GetServer(EndPoint endPoint)
        {
            return GetConnection().GetServer(endPoint);
        }

        public EndPoint[] GetEndPoints()
        {
            return GetConnection().GetEndPoints();
        }

        public void FlushDatabase(int? db = null)
        {
            var endPoints = GetEndPoints();

            foreach (var endPoint in endPoints)
            {
                GetServer(endPoint).FlushDatabase(db ?? -1); //_settings.DefaultDb);
            }
        }

        public bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action)
        {
            using (var redisLock = _redisLockFactory.Create(resource, expirationTime))
            {
                //确认取得分布式锁
                if (!redisLock.IsAcquired)
                    return false;

                action();
                return true;
            }
        }

        public void Dispose()
        {
            _connection?.Close();
            _connection?.Dispose();
            _redisLockFactory?.Dispose();
            _connection = null;
            _redisLockFactory = null;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 获得Redis管理器
        /// </summary>
        /// <returns></returns>
        protected ConnectionMultiplexer GetConnection()
        {
            if (_connection != null && _connection.IsConnected) return _connection;

            lock (_lock)
            {
                if (_connection != null && _connection.IsConnected) return _connection;

                //连接断开。 处理连接...
                _connection?.Dispose();

                //创建新的Redis连接实例
                _connection = ConnectionMultiplexer.Connect(_connectionString.Value);
            }

            return _connection;
        }

        /// <summary>
        /// 创建RedisLockFactory的实例
        /// </summary>
        protected RedisLockFactory CreateRedisLockFactory()
        {
            //获取连接密码和是否ssl方式连接
            var password = string.Empty;
            var useSsl = false;
            foreach (var option in ConnectionString.Split(',').Where(option => option.Contains('=')))
            {
                switch (option.Substring(0, option.IndexOf('=')).Trim().ToLowerInvariant())
                {
                    case "password":
                        password = option.Substring(option.IndexOf('=') + 1).Trim();
                        break;
                    case "ssl":
                        bool.TryParse(option.Substring(option.IndexOf('=') + 1).Trim(), out useSsl);
                        break;
                }
            }

            return new RedisLockFactory(GetEndPoints().Select(endPoint => new RedisLockEndPoint
            {
                EndPoint = endPoint,
                Password = password,
                Ssl = useSsl
            }));
        }

        #endregion

    }
}
