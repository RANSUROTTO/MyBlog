using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Presentation.Admin.Controllers
{
    public class RegionController : Controller
    {

        public ActionResult Sidebar()
        {
            return View();
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }

    }
}