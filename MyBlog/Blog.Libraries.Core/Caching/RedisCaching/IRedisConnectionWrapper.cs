using System;
using System.Net;
using StackExchange.Redis;

namespace Blog.Libraries.Core.Caching.RedisCaching
{

    /// <summary>
    /// Redis连接管理器
    /// </summary>
    public interface IRedisConnectionWrapper : IDisposable
    {

        /// <summary>
        /// 创建一个与Redis交互的连接
        /// </summary>
        /// <param name="db">数据库号; 传递null则使用默认值</param>
        /// <returns>Redis连接</returns>
        IDatabase GetDatabase(int? db = null);

        /// <summary>
        /// Obtain a configuration API for an individual server
        /// </summary>
        /// <param name="endPoint">The network endpoint</param>
        /// <returns>Redis server</returns>
        IServer GetServer(EndPoint endPoint);

        /// <summary>
        /// Gets all endpoints defined on the server
        /// </summary>
        /// <returns>Array of endpoints</returns>
        EndPoint[] GetEndPoints();

        /// <summary>
        /// Delete all the keys of the database
        /// </summary>
        /// <param name="db">Database number; pass null to use the default value<</param>
        void FlushDatabase(int? db = null);

        /// <summary>
        /// Perform some action with Redis distributed lock
        /// </summary>
        /// <param name="resource">The thing we are locking on</param>
        /// <param name="expirationTime">The time after which the lock will automatically be expired by Redis</param>
        /// <param name="action">Action to be performed with locking</param>
        /// <returns>True if lock was acquired and action was performed; otherwise false</returns>
        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);

    }
}
