using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace Blog.Libraries.Core.Caching.RedisCaching
{

    /// <summary>
    /// Redis连接管理
    /// </summary>
    interface IRedisConnectionWrapper : IDisposable
    {

        /// <summary>
        /// 创建一个与Redis交互的连接
        /// </summary>
        /// <param name="db">数据库号; 传递null则使用默认值</param>
        /// <returns>Redis连接</returns>
        IDatabase GetDatabase(int? db = null);






    }
}
