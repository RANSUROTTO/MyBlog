using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Data.Domain.Members.Enum;

namespace Blog.Libraries.Data.Settings
{

    /// <summary>
    /// 系统对用户的设置
    /// </summary>
    public class CustomerSettings : ISettings
    {

        /// <summary>
        /// 获取或设置身份验证票据名称
        /// </summary>
        public string AuthenticationTicketName { get; set; } = "default_authentication_ticket";

        /// <summary>
        /// 指示可以使用什么账号进行登录
        /// </summary>
        public CustomerLoginPattern CustomerLoginPattern { get; set; } = CustomerLoginPattern.Full;

        /// <summary>
        /// 获取或设置一个值，指示用户是否可以检查用户名的可用性（在“我的帐户”中注册或更改时）
        /// </summary>
        public bool CheckUsernameAvailabilityEnabled { get; set; }

        /// <summary>
        /// 获取或设置一个值，指示用户是否被允许更改其用户名
        /// </summary>
        public bool AllowUsersToChangeUsernames { get; set; } = false;

        /// <summary>
        /// 客户的默认密码格式
        /// </summary>
        public PasswordFormat DefaultPasswordFormat { get; set; }

        /// <summary>
        /// 获取或设置密码散列时的客户密码格式（SHA1，MD5）
        /// </summary>
        public string HashedPasswordFormat { get; set; }

        /// <summary>
        /// 获取或设置最小密码长度
        /// </summary>
        public int PasswordMinLength { get; set; }

        /// <summary>
        /// 获取或设置锁定帐户的最大登录失败次数,设置0以禁用此功能
        /// </summary>
        public int FailedPasswordAllowedAttempts { get; set; }

        /// <summary>
        /// 获取或设置锁定用户的分钟数（用于登录失败）
        /// </summary>
        public int FailedPasswordLockoutMinutes { get; set; }

        /// <summary>
        /// 用户注册类型
        /// </summary>
        public UserRegistrationType UserRegistrationType { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示用户是否允许上传头像
        /// </summary>
        public bool AllowCustomersToUploadAvatars { get; set; }

        /// <summary>
        /// 获取或设置最大头像大小（以字节(bytes)为单位）
        /// </summary>
        public int AvatarMaximumSizeBytes { get; set; }

        /// <summary>
        /// 获取或设置一个值，指示是否显示默认用户头像
        /// </summary>
        public bool DefaultAvatarEnabled { get; set; }

        /// <summary>
        /// 用户名称显示方式
        /// </summary>
        public CustomerNameFormat CustomerNameFormat { get; set; }

        /// <summary>
        /// 获取或设置一个值，表示我们应该存储每个客户的上次访问页面URL
        /// </summary>
        public bool StoreLastVisitedPage { get; set; }

        /// <summary>
        /// 获取或设置删除访客任务运行的间隔（以分钟为单位）
        /// </summary>
        public int DeleteGuestTaskOlderThanMinutes { get; set; }

        /// <summary>
        /// 获取或设置最小年龄.如果被忽略，则为空
        /// </summary>
        public int? DateOfBirthMinimumAge { get; set; }

        /// <summary>
        /// 获取或设置一个值，指示注册是否需要接受用户隐私协议
        /// </summary>
        public bool AcceptPrivacyPolicyEnabled { get; set; }

    }

}
