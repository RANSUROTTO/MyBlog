using System;
using System.Web.Mvc;
using Blog.Libraries.Services.Permissions;

namespace Blog.Presentation.Framework.Attributes.FilterAttributes.AuthenticationFilterAttributes
{

    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {

        private readonly IRoleService _roleService;

        public AdminAuthenticationAttribute(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var actionDescriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;
            if (actionDescriptor == null) throw new ArgumentException("��Ч��action����");

            var methodName = actionDescriptor.MethodInfo.Name;
            var className = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType.FullName;

            if (_roleService.Authorize(className, methodName))
                base.OnActionExecuting(filterContext);
            else
            {
                filterContext.HttpContext.Response.RedirectToRoute("Admin_Login", new { returnUrl = "" });
                filterContext.HttpContext.Response.End();
            }
        }

    }

}
