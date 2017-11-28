using System.Web.Mvc;
using Blog.Presentation.Framework.Context;

namespace Blog.Presentation.Admin.Controllers
{

    public class HomeController : Controller
    {

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