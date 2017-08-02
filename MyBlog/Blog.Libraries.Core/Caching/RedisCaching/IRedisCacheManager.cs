using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.Caching.RedisCaching
{

    interface IRedisCacheManager : ICacheManager
    {

        #region List

        /// <summary>
        /// 将值添加到key关联的List类型的redis缓存左边
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="data">值</param>
        void ListSet(string key, object data);

        /// <summary>
        /// 通过key获取一个List类型的Redis缓存的长度
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>索引长度</returns>
        long ListGetLenth(string key);

        /// <summary>
        /// 通过key和索引长度在List类型的Redis缓存中获取精确的缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns>缓存</returns>
        T ListGetItem<T>(string key, long index);

        /// <summary>
        /// 通过key值在Redis缓存中查找List缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns>List缓存</returns>
        List<T> ListRange<T>(string key);

        /// <summary>
        /// 入队
        /// </summary>
        void ListRightSet<T>(string key, T value);

        /// <summary>
        /// 出队
        /// </summary>
        T ListRightPop<T>(string key);

        /// <summary>
        /// 入栈
        /// </summary>
        void ListLeftSet<T>(string key, T value);

        /// <summary>
        /// 出栈
        /// </summary>
        T ListLeftPop<T>(string key);

        #endregion

        #region Hash Operating

        #endregion

    }

}
