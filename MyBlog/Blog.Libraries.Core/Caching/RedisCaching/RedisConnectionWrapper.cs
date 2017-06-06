using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RedLock;
using StackExchange.Redis;

namespace Blog.Libraries.Core.Caching.RedisCaching
{
    /// <summary>
    /// Redis连接包装实现
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {

        #region Fieids


        private readonly Lazy<string> _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private volatile RedisLockFactory _redisLockFactory;
        private readonly object _lock = new object();

        #endregion

        #region Ctor


        #endregion

        #region Utilities


        #endregion

        #region Methods

        public IDatabase GetDatabase(int? db = null)
        {
            throw new NotImplementedException();
        }

        public IServer GetServer(EndPoint endPoint)
        {
            throw new NotImplementedException();
        }

        public EndPoint[] GetEndPoints()
        {
            throw new NotImplementedException();
        }

        public void FlushDatabase(int? db = null)
        {
            throw new NotImplementedException();
        }

        public bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
