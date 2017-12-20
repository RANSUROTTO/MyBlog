using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.Context;
using Blog.Presentation.Framework.Controllers;

namespace Blog.Presentation.Admin.Controllers
{

    [ControllerDescription("[Name=test,Icon=fa fa-awit,Order=0,I18n=true]", "[Name=hhh,Icon=fa fa-awit,Order=0,I18n=true]")]
    public class HomeController : AdminController
    {

        public HomeController(IWorkContext workContext) : base(workContext)
        {
        }

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