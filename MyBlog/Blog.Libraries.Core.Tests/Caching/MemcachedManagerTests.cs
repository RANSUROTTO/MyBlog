using NUnit.Framework;
using Blog.Libraries.Core.Caching.MemCaching;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.Caching
{

    [Ignore("Please configure enyim.memcached in app.config")]
    [TestFixture]
    public class MemcachedManagerTests
    {

        #region Field

        private static MemcachedManager _client;

        #endregion

        #region Ctor

        static MemcachedManagerTests()
        {
            _client = new MemcachedManager("enyim.com/memcached");
        }

        #endregion

        [Test]
        public void PassesMamcachedManager_Set_Success()
        {
            _client.Set("name", "joe", 1000);
            _client.HasKey("name").TestBeTrue();
            _client.Get<string>("name").TestEqual("joe");
            _client.Remove("name");
            _client.HasKey("name").TestBeFalse();
        }

        [Test]
        public void PassesMemcachedManager_Set_Success()
        {
            _client.Set("key1", "value1", 1000);
            _client.Set("key2", "value2", 1000);
            _client.HasKey("key1").TestBeTrue();
            _client.HasKey("key2").TestBeTrue();
            _client.Clear();
            _client.HasKey("key1").TestBeFalse();
            _client.HasKey("key2").TestBeFalse();

        }

        [Test]
        public void PassesMemcachedManager_SetLengthOfKeyGt255Bytes_Fail()
        {
            _client.Set(
                "keylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylength",
                "value", 1000);
            _client.HasKey("keylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylengthkeylength").TestBeFalse();
        }

    }

}
