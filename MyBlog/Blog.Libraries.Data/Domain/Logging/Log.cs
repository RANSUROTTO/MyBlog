using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Libraries.Data.Domain.Logging
{

    /// <summary>
    /// 代表一个日志记录
    /// </summary>
    public class Log : BaseEntity
    {

        /// <summary>
        /// 获取或设置短消息
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// 获取或设置完整消息
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// 获取或设置引发日志记录的ip地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 获取或设置引发日志记录的页面地址
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// 获取或设置请求页面前的地址
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// 获取或设置日志等级
        /// </summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>
        /// 引发日志记录的用户
        /// </summary>
        public virtual Customer Customer { get; set; }

    }

}
