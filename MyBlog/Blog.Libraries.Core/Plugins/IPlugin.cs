
namespace Blog.Libraries.Core.Plugins
{

    /// <summary>
    /// 代表整个插件的属性
    /// </summary>
    public interface IPlugin
    {

        /// <summary>
        /// 插件模型
        /// </summary>
        PluginDescriptor PluginDescriptor { get; set; }

        /// <summary>
        /// 安装插件
        /// </summary>
        void Install();

        /// <summary>
        /// 卸载插件
        /// </summary>
        void Uninstall();

    }

}
