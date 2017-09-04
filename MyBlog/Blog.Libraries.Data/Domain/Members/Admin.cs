using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Libraries.Data.Domain.Members
{

    /// <summary>
    /// 代表一个管理员
    /// </summary>
    public class Admin : BaseEntity, IAdmin
    {

        /// <summary>
        /// 获取或设置管理员昵称
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 获取或设置管理员登录密码
        /// </summary>
        public string Passwrod { get; set; }

        /// <summary>
        /// 获取或设置管理员绑定的用户
        /// </summary>
        public virtual Customer Customer { get; set; }
        ICustomer IAdmin.Customer
        {
            get { return Customer; }
            set { }
        }

    }

}
