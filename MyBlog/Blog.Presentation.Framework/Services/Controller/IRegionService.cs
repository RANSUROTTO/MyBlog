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
        /// 获取管理员菜单项集合
        /// </summary>
        /// <returns>管理员菜单项集合</returns>
        IList<AdminMenu> GetAdminMenus();

    }

}
