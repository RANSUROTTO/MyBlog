using System.Collections.Generic;
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

                foreach (var description in descriptionAttribute.Descriptions)
                {

                }

            }
            return adminMenus;
        }

        #endregion

        #region Utilities



        #endregion

    }

}
