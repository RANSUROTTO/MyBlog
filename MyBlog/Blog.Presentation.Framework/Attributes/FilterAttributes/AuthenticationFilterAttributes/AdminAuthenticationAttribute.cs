using System;
using System.Web.Mvc;

namespace Blog.Presentation.Framework.Attributes.FilterAttributes.AuthenticationFilterAttributes
{

    public class AdminAuthenticationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var actionDescriptor = filterContext.ActionDescriptor as ReflectedActionDescriptor;
            if (actionDescriptor == null) throw new ArgumentException("��Ч��action����");\

            var methodName = actionDescriptor.MethodInfo.Name;
            var className = filterContext.ActionDescriptor.ControllerDescriptor.ControllerType;

            if (true)
                base.OnActionExecuting(filterContext);
            else
            {
                filterContext.HttpContext.Response.RedirectToRoute("Admin_Login", new { returnUrl = "" });
                filterContext.HttpContext.Response.End();
            }
        }

    }

}
