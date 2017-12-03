using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Domain.Jurisdiction
{

    /// <summary>
    /// 代表一个用户权限
    /// </summary>
    public class UserRole : BaseEntity
    {

        /// <summary>
        /// 获取或设置权限字符串
        /// </summary>
        public string RoleString { get; set; }

        /// <summary>
        /// 获取或设置权限对应用户
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// 获取或设置权限对应组
        /// </summary>
        public virtual RoleGroup RoleGroup { get; set; }

    }

}
