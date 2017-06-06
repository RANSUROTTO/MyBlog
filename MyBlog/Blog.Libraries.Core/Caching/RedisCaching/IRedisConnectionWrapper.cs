using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Blog.Libraries.Core.Caching.RedisCaching
{
    /// <summary>
    /// Redis连接包装接口
    /// </summary>
    public interface IRedisConnectionWrapper : IDisposable
    {

        /// <summary>
        /// 获取与redis中的数据库的交互式连接
        /// </summary>
        /// <param name="db">数据库编号; 传递null使用默认值</param>
        /// <returns>Redis缓存数据库</returns>
        IDatabase GetDatabase(int? db = null);

        /// <summary>
        /// 获取单个服务器的配置API
        /// </summary>
        /// <param name="endPoint">网络端口</param>
        /// <returns>Redis服务</returns>
        IServer GetServer(EndPoint endPoint);

        /// <summary>
        /// 获取服务器上定义的所有端点
        /// </summary>
        /// <returns>端口数组</returns>
        EndPoint[] GetEndPoints();

        /// <summary>
        /// 删除数据库的所有键
        /// </summary>
        /// <param name="db">数据库编号; 传递null使用默认值</param>
        void FlushDatabase(int? db = null);

        /// <summary>
        /// 使用Redis分发锁执行一些操作
        /// </summary>
        /// <param name="resource">我们锁定的源</param>
        /// <param name="expirationTime">Redis自动过期的时间</param>
        /// <param name="action">通过锁定执行的操作</param>
        /// <returns>如果获得锁定并执行动作，则为真; 否则为虚假</returns>
        bool PerformActionWithLock(string resource, TimeSpan expirationTime, Action action);

    }
}
