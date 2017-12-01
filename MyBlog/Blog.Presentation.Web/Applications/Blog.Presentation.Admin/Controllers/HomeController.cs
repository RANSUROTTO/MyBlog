using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Presentation.Framework.Context;
using Blog.Presentation.Framework.Controllers;

namespace Blog.Presentation.Admin.Controllers
{

    public class HomeController : AdminController
    {

        #region Constructor

        public HomeController(IWorkContext workContext) : base(workContext)
        {
        }

        #endregion

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.PageContext = new PageContext
            {
                Title = "Index"
            };
            return View();
        }

    }

}