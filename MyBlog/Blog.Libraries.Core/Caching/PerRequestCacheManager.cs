using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Libraries.Core.Caching
{
    /// <summary>
    /// 代表HTTP请求中缓存的管理类（短期缓存）
    /// </summary>
    public class PerRequestCacheManager : ICacheManager
    {

        #region Fields

        private readonly HttpContextBase _context;

        #endregion

        #region Ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">Http请求上下文</param>
        public PerRequestCacheManager(HttpContextBase context)
        {
            this._context = context;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 获取Http请求过程中的共享数据 键/值 集合
        /// </summary>
        protected virtual IDictionary GetItems()
        {
            return _context?.Items;
        }

        #endregion

        #region Methods

        public virtual T Get<T>(string key)
        {
            var items = GetItems();
            if (items == null)
                return default(T);

            return (T)items[key];
        }

        public virtual void Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return;

            if (data != null)
            {
                if (items.Contains(key))
                    items[key] = data;
                else
                    items.Add(key, data);
            }
        }

        public virtual bool Any(string key)
        {
            var items = GetItems();

            return items?[key] != null;
        }

        public virtual void Remove(string key)
        {
            var items = GetItems();

            items?.Remove(key);
        }

        public virtual void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;

            this.RemoveByPattern(pattern, items.Keys.Cast<object>().Select(p => p.ToString()));
        }

        public virtual void Clear()
        {
            var items = GetItems();

            items?.Clear();
        }

        public virtual void Dispose()
        {

        }

        #endregion

    }
}
