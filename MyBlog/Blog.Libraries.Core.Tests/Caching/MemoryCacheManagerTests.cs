using Blog.Libraries.Core.Caching;
using Blog.Tests;
using NUnit.Framework;

namespace Blog.Libraries.Core.Tests.Caching
{

    [TestFixture]
    public class MemoryCacheManagerTests
    {

        #region Field

        private static MemoryCacheManager _cacheManager;

        #endregion

        #region Ctor

        static MemoryCacheManagerTests()
        {
            _cacheManager = new MemoryCacheManager();
        }

        #endregion

        [Test]
        public void PassesMemoryCacheManager_Set_Success()
        {
            _cacheManager.Set("name", "joe", int.MaxValue);
            _cacheManager.HasKey("name").TestBeTrue();
            _cacheManager.Get<string>("name").TestEqual("joe");
            _cacheManager.Remove("name");
            _cacheManager.HasKey("name").TestBeFalse();
        }

        [Test]
        public void PassesMemoryCacheManager_Clear_Success()
        {
            _cacheManager.Set("key1", "value1", int.MaxValue);
            _cacheManager.Set("key2", "value2", int.MaxValue);
            _cacheManager.HasKey("key1").TestBeTrue();
            _cacheManager.HasKey("key2").TestBeTrue();
            _cacheManager.Clear();
            _cacheManager.HasKey("key1").TestBeFalse();
            _cacheManager.HasKey("key2").TestBeFalse();
        }

    }

}
