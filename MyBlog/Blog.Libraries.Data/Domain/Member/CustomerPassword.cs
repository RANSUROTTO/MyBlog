using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Member.Enum;

namespace Blog.Libraries.Data.Domain.Member
{

    public class CustomerPassword : BaseEntity
    {

        /// <summary>
        /// 获取或设置密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 获取或设置密码格式
        /// </summary>
        public PasswordFormat PasswordFormat { get; set; }

        /// <summary>
        /// 获取或设置Hash盐值
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// 获取或设置所属用户
        /// </summary>
        public virtual Customer Customer { get; set; }

    }

}
