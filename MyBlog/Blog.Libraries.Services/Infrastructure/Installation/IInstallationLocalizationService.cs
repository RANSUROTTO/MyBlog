using System.Collections.Generic;

namespace Blog.Libraries.Services.Infrastructure.Installation
{

    /// <summary>
    /// 安装时区域化服务接口
    /// </summary>
    public interface IInstallationLocalizationService
    {

        /// <summary>
        /// 获取区域资源值
        /// </summary>
        /// <param name="resourceName">资源键</param>
        /// <returns>资源值</returns>
        string GetResource(string resourceName);

        /// <summary>
        /// 获取安装页面当前的语言
        /// </summary>
        /// <returns>安装使用语言</returns>
        InstallationLanguage GetCurrentLanguage();

        /// <summary>
        /// 保存安装页面当前使用的语言
        /// </summary>
        /// <param name="languageCode">语言代码</param>
        void SaveCurrentLanguage(string languageCode);

        /// <summary>
        /// 获取安装页面所有可用语言
        /// </summary>
        /// <returns>可用语言列表</returns>
        IList<InstallationLanguage> GetAvailableLanguage();

    }

}
