using System.Web.Mvc;

namespace Blog.Presentation.Framework.Attributes.FilterAttributes
{

    public class UserAuthenticationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

    }

}
