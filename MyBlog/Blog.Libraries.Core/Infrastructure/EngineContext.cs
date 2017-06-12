using System.Configuration;
using System.Runtime.CompilerServices;
using Blog.Libraries.Core.Configuration;

namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 提供访问项目引擎单例的实例
    /// </summary>
    public class EngineContext
    {

        #region Properties

        /// <summary>
        /// 获取使用服务的单例引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    //Initialize
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化项目引擎的静态工厂
        /// 该类保持线程同步
        /// </summary>
        /// <param name="forceRecreate">即使工厂以前已初始化，也创建一个新的工厂实例</param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                Singleton<IEngine>.Instance = new SiteEngine();
                //Read WebConfig XmlNode
                var config = ConfigurationManager.GetSection("WebConfig") as WebConfig;
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// 将静态引擎实例设置为所提供的引擎
        /// </summary>
        /// <param name="engine">使用的引擎</param>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

    }
}
