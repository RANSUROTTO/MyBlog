using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Collections.Generic;
using Blog.Libraries.Core.Helper;
using System.Text.RegularExpressions;
using Blog.Libraries.Core.Infrastructure;

namespace Blog.Libraries.Services.Infrastructure.Installation
{

    public class InstallationLocalizationService : IInstallationLocalizationService
    {

        /// <summary>
        /// 安装页面存储语言选择的Cookie的名称
        /// </summary>
        private const string LanguageCookieName = "ransurotto.cn.installation.lang";

        /// <summary>
        /// 安装页面可用语言
        /// </summary>
        private IList<InstallationLanguage> _availableLanguages;

        public string GetResource(string resourceName)
        {
            var language = GetCurrentLanguage();
            if (language == null)
                return resourceName;

            var resourceValue = language.Resources
                .Where(p => p.Name.Equals(resourceName, StringComparison.InvariantCultureIgnoreCase))
                .Select(p => p.Value)
                .FirstOrDefault();
            if (string.IsNullOrEmpty(resourceValue))
                return resourceName;

            return resourceValue;
        }

        public InstallationLanguage GetCurrentLanguage()
        {
            //1.首选使用用户手动选择的语言
            var httpContext = EngineContext.Current.Resolve<HttpContextBase>();

            string cookieLanguageCode = string.Empty;

            var cookie = httpContext.Request.Cookies[LanguageCookieName];
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
                cookieLanguageCode = cookie.Value;

            var language = _availableLanguages
                .FirstOrDefault(l => l.Code.Equals(cookieLanguageCode, StringComparison.InvariantCultureIgnoreCase));
            if (language != null)
                return language;

            //2.次选用户浏览器的语言
            if (httpContext.Request.UserLanguages != null)
            {
                var userLanguage = httpContext.Request.UserLanguages.FirstOrDefault();
                if (!string.IsNullOrEmpty(userLanguage))
                {
                    language = _availableLanguages
                        .FirstOrDefault(p => userLanguage.StartsWith(p.Code, StringComparison.InvariantCultureIgnoreCase));
                }
            }
            if (language != null)
                return language;

            //3.采用应用默认语言
            language = _availableLanguages.FirstOrDefault(p => p.IsDefault);
            if (language != null)
                return language;

            language = _availableLanguages.FirstOrDefault();
            return language;
        }

        public void SaveCurrentLanguage(string languageCode)
        {
            var httpContext = EngineContext.Current.Resolve<HttpContextBase>();

            var cookie = new HttpCookie(LanguageCookieName)
            {
                HttpOnly = true,
                Value = languageCode,
                Expires = DateTime.Now.AddHours(24)
            };

            httpContext.Response.Cookies.Remove(LanguageCookieName);
            httpContext.Response.Cookies.Add(cookie);
        }

        public IList<InstallationLanguage> GetAvailableLanguage()
        {
            if (_availableLanguages == null)
            {
                _availableLanguages = new List<InstallationLanguage>();
                foreach (var filePath in Directory.EnumerateFiles(CommonHelper.MapPath("~/App_Data/Localization/Installation/"), "*.xml"))
                {
                    if (filePath == null)
                        continue;

                    var xmlDocument = new XmlDocument();
                    xmlDocument.Load(filePath);

                    var languageCode = "";
                    //读取文件名格式为 installation.languageName.xml 的语言文件
                    var r = new Regex(Regex.Escape("installation.") + "(.*?)" + Regex.Escape(".xml"));
                    var matches = r.Matches(Path.GetFileName(filePath));
                    foreach (Match match in matches)
                        languageCode = match.Groups[1].Value;

                    var languageName = xmlDocument.SelectSingleNode(@"//Language").Attributes["Name"].InnerText.Trim();

                    var isDefaultAttribute = xmlDocument.SelectSingleNode(@"//Language").Attributes["IsDefault"];
                    var isDefault = isDefaultAttribute != null && Convert.ToBoolean(isDefaultAttribute.InnerText.Trim());

                    var isRightToLeftAttribute = xmlDocument.SelectSingleNode(@"//Language").Attributes["IsRightToLeft"];
                    var isRightToLeft = isRightToLeftAttribute != null && Convert.ToBoolean(isRightToLeftAttribute.InnerText.Trim());

                    InstallationLanguage language = new InstallationLanguage
                    {
                        Code = languageCode,
                        Name = languageName,
                        IsDefault = isDefault,
                        IsRightToLeft = isRightToLeft
                    };

                    //加载语言的资源
                    foreach (XmlNode resNode in xmlDocument.SelectNodes(@"//Language/LocaleResource"))
                    {
                        var resNameAttribute = resNode.Attributes["Name"];
                        var resValueNode = resNode.SelectSingleNode("Value");

                        var resourceName = resNameAttribute.Value.Trim();
                        var resourceValue = resValueNode.InnerText.Trim();

                        language.Resources.Add(new InstallationLocaleResource
                        {
                            Name = resourceName,
                            Value = resourceValue
                        });
                    }
                    _availableLanguages.Add(language);
                }
            }
            return _availableLanguages;
        }

    }

}
