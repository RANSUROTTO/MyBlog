using System.Web.Mvc;
using Blog.Presentation.Framework.Context;

namespace Blog.Presentation.Framework.Controllers
{

    public abstract class PublicController : BaseController
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ViewBag.PageContext = new PageContext
            {
                Title = "Title",
                Author = "Author",
                Keywords = "Keywords",
                Description = "Description",
            };
            base.OnActionExecuting(filterContext);
        }

    }

}
