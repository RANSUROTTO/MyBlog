using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Configuration;

namespace Blog.Libraries.Data.Settings
{

    /// <summary>
    /// 安全设置
    /// </summary>
    public class SecuritySettings : ISettings
    {

        /// <summary>
        /// 获取或设置加密密钥
        /// </summary>
        public string EncryptionKey { get; set; } = "RANSUROTTOENCKEY";

        /// <summary>
        /// 获取或设置允许登录管理员后台区域的IP地址列表
        /// </summary>
        public List<string> AllowedLoginAdminAreaIpAddresses { get; set; }




    }

}
