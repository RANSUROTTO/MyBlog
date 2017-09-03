using System;

namespace Blog.Libraries.Core.Domain.Members
{

    /// <summary>
    /// 代表一个用户
    /// </summary>
    public interface ICustomer
    {

        /// <summary>
        /// 获取或设置用户名
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 获取或设置电子邮箱
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否要求用户重新登录
        /// 用于验证自动登录
        /// </summary>
        bool RequireReLogin { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示失败的登录尝试次数
        /// 密码错误
        /// </summary>
        int FailedLoginAttempts { get; set; }

        /// <summary>
        /// 获取或设置一个时间,该用户在此之前不能登录
        /// 锁定
        /// </summary>
        DateTime? CannotLoginUntilDate { get; set; }

        /// <summary>
        /// 获取或设置用户最后操作的ip地址
        /// </summary>
        string LastIpAddress { get; set; }

        /// <summary>
        /// 获取或设置用户最后登录时间
        /// </summary>
        DateTime LastLoginDate { get; set; }

    }

}
