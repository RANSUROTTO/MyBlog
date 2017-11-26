using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Presentation.Framework.Context;

namespace Blog.Presentation.Admin.Controllers
{

    public class HomeController : Controller
    {

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