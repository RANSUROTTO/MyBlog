using Blog.Libraries.Core.Configuration;

namespace Blog.Libraries.Data.Settings
{

    /// <summary>
    /// 系统对区域化的设置
    /// </summary>
    public class LocalizationSettings : ISettings
    {

        /// <summary>
        /// 获取或设置管理员区域默认语言ID
        /// </summary>
        public long DefaultAdminLanguageId { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否使用图像选择语言
        /// </summary>
        public bool UseImageForLanguageSelection { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否启用了具有多种语言的SEO友好URL
        /// </summary>
        public bool SeoFriendlyUrlsForLanguagesEnabled { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否应该由客户区域检测当前语言（浏览器语言设置）
        /// </summary>
        public bool AutomaticallyDetectLanguage { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否在应用启动时加载所有区域语言资源
        /// </summary>
        public bool LoadAllLocaleRecordsOnStartup { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否在应用启动时加载所有区域属性
        /// </summary>
        public bool LoadAllLocalizedPropertiesOnStartup { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否在应用程序启动时加载所有搜索引擎的友好名称
        /// </summary>
        public bool LoadAllUrlRecordsOnStartup { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否忽略管理员区域的RTL语言属性
        /// </summary>
        public bool IgnoreRtlPropertyForAdminArea { get; set; }

    }

}
