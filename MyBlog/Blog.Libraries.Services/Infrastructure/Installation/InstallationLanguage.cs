using System.Collections.Generic;

namespace Blog.Libraries.Services.Infrastructure.Installation
{

    /// <summary>
    /// 代表一个安装时可用的语言
    /// </summary>
    public class InstallationLanguage
    {
        public InstallationLanguage()
        {
            Resources = new List<InstallationLocaleResource>();
        }

        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsDefault { get; set; }
        public bool IsRightToLeft { get; set; }

        public List<InstallationLocaleResource> Resources { get; protected set; }
    }


    /// <summary>
    /// 代表一个安装时可用语言资源
    /// </summary>
    public class InstallationLocaleResource
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

}
