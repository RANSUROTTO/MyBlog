using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.CommonModel;
using Blog.Presentation.Framework.Controllers;
using Blog.Presentation.Framework.Services.Permissions;

namespace Blog.Presentation.Framework.Services.Controller
{

    public class AdminControllerService : IAdminControllerService
    {

        #region fields

        private readonly ITypeFinder _typeFinder;
        private readonly IRoleService _roleService;


        #endregion

        #region Constructor

        public AdminControllerService(ITypeFinder typeFinder, IRoleService roleService)
        {
            _typeFinder = typeFinder;
            _roleService = roleService;
        }

        #endregion

        #region Methods

        public IList<AdminMenu> GetAdminMenus()
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
        }

        #endregion

        #region Utilities

        private List<AdminMenu> IterationToMenu(List<AdminMenu> menuList, ControllerDescriptionAttribute.ControllerDescription description)
        {
            if (menuList.Any(p => p.Name == description.Name))
                return menuList.First(p => p.Name == description.Name).Children.ToList();

            var adminMenum = new AdminMenu
            {
                Name = description.Name,
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
