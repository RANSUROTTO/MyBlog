using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Presentation.Framework.Context;

namespace Blog.Presentation.Framework.Controllers
{

    public abstract class PublicController : BaseController
    {

        #region fields

        private readonly IWorkContext _workContext;

        #endregion

        #region Constructor

        protected PublicController(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        #endregion

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
