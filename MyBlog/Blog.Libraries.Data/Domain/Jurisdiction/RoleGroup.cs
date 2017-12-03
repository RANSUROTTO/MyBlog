using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Jurisdiction
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

    }

}
