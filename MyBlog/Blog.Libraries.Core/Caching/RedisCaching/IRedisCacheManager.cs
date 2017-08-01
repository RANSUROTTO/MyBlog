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
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        T ListGetItem<T>(string key, long index);





        #endregion

        #region Hash Operating

        #endregion

    }

}
