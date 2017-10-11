using System.Collections.Generic;
using System.Web.Mvc;

namespace Blog.Presentation.Web.Models.Install
{

    public class InstallModel
    {

        public InstallModel()
        {
            this.AvailableLanguages = new List<SelectListItem>();
        }

        /// <summary>
        /// 获取或设置管理员邮箱地址
        /// </summary>
        public string AdminEmail { get; set; }

        /// <summary>
        /// 获取或设置管理员密码
        /// </summary>
        public string AdminPassword { get; set; }

        /// <summary>
        /// 获取或设置管理员确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否导入测试数据
        /// </summary>
        public bool InstallSampleData { get; set; }

        /// <summary>
        /// 获取或设置数据库提供者(使用的数据库的名称,page default = mysql)
        /// </summary>
        public string DataProvider { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示当数据库不存在时是否创建数据库
        /// </summary>
        public bool NotExistCreateDatabase { get; set; }

        /// <summary>
        /// 获取或设置输入连接字符串的方式
        /// sqlconnectioninfo_values
        /// sqlconnectioninfo_raw
        /// </summary>
        public string SqlConnectionInfo { get; set; }

        /// <summary>
        /// 获取或设置数据库服务实例名
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 获取或设置数据库名称
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// 获取或设置数据库服务授权用户名
        /// </summary>
        public string ServerUsername { get; set; }

        /// <summary>
        /// 获取或设置数据库服务授权用户名密码
        /// </summary>
        public string ServerUserPassword { get; set; }

        /// <summary>
        /// 获取或设置数据库连接字符串
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        /// <summary>
        /// 获取或设置安装时可用的语言列表
        /// </summary>
        public List<SelectListItem> AvailableLanguages { get; set; }

    }

}