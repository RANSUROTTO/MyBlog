using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using Blog.Libraries.Services.Members;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.CommonModel;
using Blog.Presentation.Framework.Controllers;
using Blog.Presentation.Framework.Services.Permissions;

namespace Blog.Presentation.Framework.Services.Controller
{

    public class RegionService : IRegionService
    {

        #region Fields

        private readonly IAdminService _adminService;
        private readonly ITypeFinder _typeFinder;
        private readonly IRoleService _roleService;
        private readonly ICacheManager _cacheManager;
        private readonly HttpContextBase _httpContextBase;
        private const string AdminMenuCachekey = "ransurotto.com.region.menu_{0}";

        #endregion

        #region Constructor

        public RegionService(IAdminService adminService, ITypeFinder typeFinder, IRoleService roleService, ICacheManager cacheManager, HttpContextBase httpContextBase)
        {
            _adminService = adminService;
            _typeFinder = typeFinder;
            _roleService = roleService;
            _cacheManager = cacheManager;
            _httpContextBase = httpContextBase;
        }

        #endregion

        #region Methods

        public List<AdminMenu> GetAdminMenus(long adminId)
        {
            var admin = _adminService.GetAdminById(adminId);
            var adminMenuKey = string.Format(AdminMenuCachekey, adminId);

            return _cacheManager.Get(adminMenuKey, () =>
            {
                var urlHelper = new UrlHelper(_httpContextBase.Request.RequestContext);
                AdminMenu adminMenu = new AdminMenu { Name = "root" };

                var adminControllers = _typeFinder.FindClassesOfType<AdminController>();
                foreach (var adminController in adminControllers)
                {
                    var descriptionAttribute = adminController.GetCustomAttribute<ControllerDescriptionAttribute>();
                    if (descriptionAttribute == null)
                        continue;

                    var controllerName = adminController.Name.Replace("Controller", "");
                    var url = urlHelper.Action("Index", controllerName, new { Area = "Admin" });

                    //验证权限
                    if (!_roleService.Authorize("Admin", controllerName, "Index", admin))
                        continue;

                    //伪迭代
                    var lastIterationMenu = adminMenu;
                    foreach (var description in descriptionAttribute.Descriptions)
                    {
                        lastIterationMenu = IterationToMenu(lastIterationMenu, description);
                    }
                    lastIterationMenu.Url = url;

                }
                return adminMenu.Children;
            });
        }

        #endregion

        #region Utilities

        private AdminMenu IterationToMenu(AdminMenu menu, ControllerDescriptionAttribute.ControllerDescription description)
        {
            var adminMenu = new AdminMenu
            {
                Name = description.I18N ? description.Name + "" : description.Name,
                I18N = description.I18N,
                Icon = description.Icon,
                Order = description.Order,
                Children = new List<AdminMenu>()
            };

            if (menu.Children == null)
            {
                menu.Children = new List<AdminMenu> { adminMenu };
                return menu.Children.First(p => p.Name == adminMenu.Name);
            }

            if (menu.Children.Any(p => p.Name == adminMenu.Name))
                return menu.Children.First(p => p.Name == adminMenu.Name);

            menu.Children.Add(adminMenu);
            return menu.Children.First(p => p.Name == adminMenu.Name);
        }

        #endregion

    }

}
