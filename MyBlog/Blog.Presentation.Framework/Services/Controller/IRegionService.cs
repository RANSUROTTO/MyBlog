using System.Collections.Generic;
using Blog.Presentation.Framework.CommonModel;

namespace Blog.Presentation.Framework.Services.Controller
{

    /// <summary>
    /// 管理员控制器业务
    /// </summary>
    public interface IRegionService
    {

        /// <summary>
        /// 获取指定管理员可见菜单项集合
        /// debug模式下获取所有可见菜单项集合
        /// </summary>
        /// <param name="adminId">管理员ID</param>
        /// <returns>管理员可见菜单项集合</returns>
        List<AdminMenu> GetAdminMenus(long adminId);

    }

}
