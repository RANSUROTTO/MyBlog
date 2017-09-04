using System;
using System.Collections.Generic;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Domain.Members;
using Blog.Libraries.Data.Domain.Logging;
using System.Linq;

namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 代表一个用户
    /// </summary>
    public class Customer : BaseEntity, ICustomer
    {

        /// <summary>
        /// 获取或设置登录用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 获取或设置登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置电子邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否要求用户重新登录
        /// 用于验证自动登录
        /// </summary>
        public bool RequireReLogin { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示失败的登录尝试次数
        /// 密码错误
        /// </summary>
        public int FailedLoginAttempts { get; set; }

        /// <summary>
        /// 获取或设置一个时间,该用户在此之前不能登录
        /// 锁定
        /// </summary>
        public DateTime? CannotLoginUntilDate { get; set; }

        /// <summary>
        /// 获取或设置用户最后登录的ip地址
        /// </summary>
        public string LastLoginIpAddress { get; set; }

        /// <summary>
        /// 获取或设置用户最后登录时间
        /// </summary>
        public DateTime LastLoginDate { get; set; }



        private ICollection<Log> _logs;

        /// <summary>
        /// 获取或设置该用户引发的日志记录
        /// </summary>
        public virtual ICollection<Log> Logs
        {
            get { return _logs?.Where(p => !p.IsDeleted).ToList() ?? (_logs = new List<Log>()); }
            set { _logs = value; }
        }

    }

}
