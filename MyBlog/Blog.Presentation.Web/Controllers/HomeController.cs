using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using System.Reflection;
using System.Web.Mvc.Routing;

namespace Blog.Presentation.Web.Controllers
{

    public class HomeController : Controller
    {

        private IWebHelper WebHelper { get { return new WebHelper(HttpContext); } }

        public ActionResult Index()
        {

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CommonHelper.MapPath:" + CommonHelper.MapPath("~"));
            sb.AppendLine("WebHelper.GetCurrentIpAddress:" + WebHelper.GetCurrentIpAddress());
            sb.AppendLine("WebHelper.GetThisPageUrl:" + WebHelper.GetThisPageUrl(true));
            sb.AppendLine("WebHelper.IsCurrentConnectionSecured:" + WebHelper.IsCurrentConnectionSecured());
            sb.AppendLine("WebHelper.GetHost:" + WebHelper.GetHost(WebHelper.IsCurrentConnectionSecured()));
            sb.AppendLine("WebHelper.GetLocation:" + WebHelper.GetLocation());

            return Content(sb.ToString());
        }

    }

}