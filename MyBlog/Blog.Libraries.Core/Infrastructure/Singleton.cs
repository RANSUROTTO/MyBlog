using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 提供存储所有单例对象的基类
    /// </summary>
    public class Singleton
    {

        #region Properties

        /// <summary>
        /// 存储单例实例的字典
        /// </summary>
        public static IDictionary<Type, object> AllSingletons { get; }

        #endregion

        #region Ctor

        static Singleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        #endregion

    }



}
