
namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 用户注册类型
    /// </summary>
    public enum UserRegistrationType : byte
    {

        /// <summary>
        /// 标准注册
        /// </summary>
        Standard = 1,

        /// <summary>
        /// 注册后需要邮箱验证
        /// </summary>
        EmailValidation = 2,

        /// <summary>
        /// 需要通过管理员批准
        /// </summary>
        AdminApproval = 3,

        /// <summary>
        /// 禁用注册
        /// </summary>
        Disabled = 4,

    }

}
