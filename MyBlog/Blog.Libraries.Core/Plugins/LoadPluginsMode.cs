
namespace Blog.Libraries.Core.Plugins
{

    /// <summary>
    /// 加载插件的模式
    /// </summary>
    public enum LoadPluginsMode
    {

        /// <summary>
        /// All (InstalledOnly & NotInstalledOnly)
        /// </summary>
        All,

        /// <summary>
        /// 仅安装
        /// </summary>
        InstalledOnly,

        /// <summary>
        /// 不仅只安装
        /// </summary>
        NotInstalledOnly

    }

}
