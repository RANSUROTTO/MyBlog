using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Caching.RedisCaching;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Caching
{

    /// <summary>
    /// Redis单元测试
    /// 运行前请注释RedisCacheManager构造函数中的_perRequestCacheManager依赖解析
    /// </summary>
    [TestFixture]
    [Ignore("Annotated RedisCacheManager.cs constructor within the specified code")]
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
            _redisConnectionWrapper = new RedisConnectionWrapper("127.0.0.1:6379,allowAdmin=true");
            _redisCacheManager = new RedisCacheManager(_redisConnectionWrapper);
        }

        #endregion

        [Test]
        public void PassesRedisCacheManager_SetString_Success()
        {
            _redisCacheManager.Set("username", "Joe San", int.MaxValue);
            _redisCacheManager.HasKey("username").TestBeTrue();
            _redisCacheManager.Get<string>("username").TestEqual("Joe San", "redis cache:key[username] cache value not equal 'Joe San'");
            _redisCacheManager.Remove("username");
            _redisCacheManager.HasKey("username").TestBeFalse();
        }

        [Test]
        public void PassesRedisCacheManager_Clear_Success()
        {
            _redisCacheManager.Set("clear_test", "test_value", int.MaxValue);
            _redisCacheManager.HasKey("clear_test").TestBeTrue();
            _redisCacheManager.Clear();
            _redisCacheManager.HasKey("clear_test").TestBeFalse();
        }

    }

}
