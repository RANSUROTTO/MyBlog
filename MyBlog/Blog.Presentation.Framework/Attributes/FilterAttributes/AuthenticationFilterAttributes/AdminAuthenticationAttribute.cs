using System;
using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Infrastructure;
using Blog.Presentation.Framework.Services.Permissions;

namespace Blog.Presentation.Framework.Attributes.FilterAttributes.AuthenticationFilterAttributes
{

    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {

        #region Fields

        private readonly IRoleService _roleService;
        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        public AdminAuthenticationAttribute()
        {
            if (_roleService == null)
                _roleService = EngineContext.Current.Resolve<IRoleService>();
            if (_workContext == null)
                _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        public AdminAuthenticationAttribute(IRoleService roleService, IWorkContext workContext)
        {
            _roleService = roleService;
            _workContext = workContext;
        }

        #endregion

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_workContext.Admin == null)
                RedirectToLogin(filterContext);

            var areaName = filterContext.RouteData.Values["Area"].ToString();
            var controllerName = filterContext.RouteData.Values["Controller"].ToString();
            var actionName = filterContext.RouteData.Values["Action"].ToString();

            if (_roleService.Authorize(areaName, controllerName, actionName))
                base.OnActionExecuting(filterContext);
            else
                RedirectToLogin(filterContext);

        }

        private void RedirectToLogin(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.RedirectToRoute("Admin_Login", new { returnUrl = "" });
            filterContext.HttpContext.Response.End();
        }

    }

}
