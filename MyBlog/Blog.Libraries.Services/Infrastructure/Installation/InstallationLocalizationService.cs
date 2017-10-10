using System;
using System.Collections.Generic;
using System.IO;
using Blog.Libraries.Core.Helper;

namespace Blog.Libraries.Services.Infrastructure.Installation
{

    public class InstallationLocalizationService : IInstallationLocalizationService
    {

        /// <summary>
        /// 安装页面存储语言选择的Cookie的名称
        /// </summary>
        private const string LanguageCookieName = "ransurotto.installation.lang";

        /// <summary>
        /// 安装页面可用语言
        /// </summary>
        private IList<InstallationLanguage> _availableLanguages;

        public string GetResource(string resourceName)
        {
            throw new NotImplementedException();
        }

        public InstallationLanguage GetCurrentLanguage()
        {
            throw new NotImplementedException();
        }

        public void SaveCurrentLanguage(string languageCode)
        {
            throw new NotImplementedException();
        }

        public IList<InstallationLanguage> GetAvailableLanguage()
        {
            if (_availableLanguages == null)
            {
                _availableLanguages = new List<InstallationLanguage>();
                foreach (var filePath in Directory.EnumerateFiles(CommonHelper.MapPath("~/App_Data/Localization/Installation/"), "*.xml"))
                {

                }

            }
            return _availableLanguages;
            throw new NotImplementedException();
        }

    }

}
