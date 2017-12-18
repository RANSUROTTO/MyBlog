using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using Blog.Libraries.Data.Domain.Member;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.CommonModel;
using Blog.Presentation.Framework.Controllers;
using Blog.Presentation.Framework.Services.Permissions;

namespace Blog.Presentation.Framework.Services.Controller
{

    public class RegionService : IRegionService
    {

        #region Fields

        private readonly ITypeFinder _typeFinder;
        private readonly IRoleService _roleService;
        private readonly IWorkContext _workContext;
        private readonly ICacheManager _cacheManager;
        private const string AdminMenuCachekey = "ransurotto.com.region.menu_{0}";

        #endregion

        #region Constructor

        public RegionService(ITypeFinder typeFinder, IRoleService roleService, IWorkContext workContext, ICacheManager cacheManager)
        {
            _typeFinder = typeFinder;
            _roleService = roleService;
            _workContext = workContext;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<AdminMenu> GetAdminMenus()
        {
            if (_workContext.Admin == null)
                throw new Exception("获取管理员菜单需要已登录管理员身份");

            var adminMenuKey = string.Format(AdminMenuCachekey, (_workContext.Admin as Admin).Id);

            return _cacheManager.Get(adminMenuKey, () =>
            {
                var adminMenus = new List<AdminMenu>();

                var adminControllers = _typeFinder.FindClassesOfType<AdminController>();
                foreach (var adminController in adminControllers)
                {
                    if (!_roleService.Authorize("Admin", adminController.Name.Replace("Controller", ""), "Index"))
                        continue;

                    var descriptionAttribute = adminController.GetCustomAttribute<ControllerDescriptionAttribute>();
                    if (descriptionAttribute == null)
                        continue;

                    var currentMenus = adminMenus;
                    foreach (var description in descriptionAttribute.Descriptions)
                    {
                        currentMenus = IterationToMenu(currentMenus, description);
                    }

                }
                return adminMenus;
            });
        }

        #endregion

        #region Utilities

        private List<AdminMenu> IterationToMenu(List<AdminMenu> menuList, ControllerDescriptionAttribute.ControllerDescription description)
        {
            if (menuList.Any(p => p.Name == description.Name))
                return menuList.First(p => p.Name == description.Name).Children.ToList();

            var adminMenum = new AdminMenu
            {
                Name = description.I18N ? description.Name + "" : description.Name,
                I18N = description.I18N,
                Icon = description.Icon,
                Order = description.Order,
                Children = new List<AdminMenu>()
            };
            menuList.Add(adminMenum);
            return adminMenum.Children.ToList();
        }

        #endregion

    }

}
