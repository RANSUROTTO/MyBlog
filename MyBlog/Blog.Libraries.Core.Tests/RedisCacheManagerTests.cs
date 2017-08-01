using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Caching.RedisCaching;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Core.Infrastructure.DependencyManagement;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests
{

    /// <summary>
    /// Redis单元测试
    /// 运行前请注释RedisCacheManager构造函数中的_perRequestCacheManager依赖解析
    /// </summary>
    [TestFixture]
    public class RedisCacheManagerTests
    {

        #region Field

        private static IRedisConnectionWrapper _redisConnectionWrapper;
        private static readonly ICacheManager _redisCacheManager;

        #endregion

        #region Ctor

        /// <summary>
        /// static Ctor
        /// </summary>
        static RedisCacheManagerTests()
        {
            _redisConnectionWrapper = new RedisConnectionWrapper("127.0.0.1:6379");
            _redisCacheManager = new RedisCacheManager(_redisConnectionWrapper);
        }

        #endregion

        [Test]
        public void PassesRedisCacheManager_SetString_Success()
        {
            _redisCacheManager.Set("username", "Joe San", 2400);
            _redisCacheManager.HasKey("username").TestBeTrue();
            _redisCacheManager.Get<string>("username").TestEqual("Joe San", "redis cache:key[username] cache value not equal 'Joe San'");
            _redisCacheManager.Remove("username");
            _redisCacheManager.HasKey("username").TestBeFalse();
        }





    }

}
