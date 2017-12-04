using System.Collections.Generic;
using System.Linq;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Permissions
{

    /// <summary>
    /// 代表一个权限组
    /// </summary>
    public class RoleGroup : BaseEntity
    {

        /// <summary>
        /// 获取或设置权限组的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置权限组的备注信息
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 获取或设置权限字符串
        /// </summary>
        public string RoleString { get; set; }


        private ICollection<UserRole> _userRoles;
        /// <summary>
        /// 获取或设置对应用户权限
        /// </summary>
        public virtual ICollection<UserRole> UserRoles { get { return _userRoles?.Where(p => !p.IsDeleted).ToList(); } set { _userRoles = value; } }

    }

}
