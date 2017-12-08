using System;
using System.Xml;

namespace Blog.Libraries.Core.Configuration
{
    public class WebConfig : BaseConfig<WebConfig>
    {

        /// <summary>
        /// 创建一个配置处理程序
        /// </summary>
        /// <param name="section">配置文件WebCofig xml父节点</param>
        public override WebConfig Create(XmlNode section)
        {
            Config = new WebConfig();

            //初始化属性值
            var debugNode = section.SelectSingleNode("Debug");
            Config.Debug = GetBool(debugNode, "Enable");

            var startupNode = section.SelectSingleNode("Startup");
            Config.IgnoreStartupTasks = GetBool(startupNode, "IgnoreStartupTasks");

            var clusterNode = section.SelectSingleNode("Cluster");
            Config.OpenClusterPattern = GetBool(clusterNode, "OpenClusterPattern");

            var redisCachingNode = section.SelectSingleNode("RedisCaching");
            Config.RedisCachingEnable = GetBool(redisCachingNode, "Enable");
            Config.RedisCachingConfig = GetString(redisCachingNode, "ConfigString");

            var memcachingNode = section.SelectSingleNode("MemCaching");
            Config.MemcachedEnable = GetBool(memcachingNode, "Enable");

            return Config;
        }

        #region Properties

        /// <summary>
        /// 调试模式[部分功能将沉默：权限等]
        /// </summary>
        public bool Debug { get; set; }

        /// <summary>
        /// 是否忽略运行应用程序启动任务
        /// </summary>
        public bool IgnoreStartupTasks { get; set; }

        /// <summary>
        /// 是否开启集群运行方式
        /// more information:
        /// https://github.com/ZhengZicong/MyBlog
        /// </summary>
        public bool OpenClusterPattern { get; set; }

        /// <summary>
        /// 是否开启Redis缓存
        /// </summary>
        public bool RedisCachingEnable { get; set; }

        /// <summary>
        /// Redis缓存配置字符串
        /// </summary>
        public string RedisCachingConfig { get; set; }

        /// <summary>
        /// 是否开启Memcached缓存
        /// </summary>
        public bool MemcachedEnable { get; set; }

        #endregion

        #region Utilities

        /// <summary>
        /// 获取节点属性值 转字符串
        /// </summary>
        private string GetString(XmlNode node, string attrName)
        {
            return SetByXElement<string>(node, attrName, Convert.ToString);
        }

        /// <summary>
        /// 获取节点属性值 转布尔
        /// </summary>
        private bool GetBool(XmlNode node, string attrName)
        {
            return SetByXElement<bool>(node, attrName, Convert.ToBoolean);
        }

        /// <summary>
        /// 获取节点属性值 通过自定义转换函数转为指定类型
        /// </summary>
        private T SetByXElement<T>(XmlNode node, string attrName, Func<string, T> converter)
        {
            if (node?.Attributes == null) return default(T);
            var attr = node.Attributes[attrName];
            if (attr == null) return default(T);
            var attrVal = attr.Value;
            return converter(attrVal);
        }

        #endregion

    }
}
