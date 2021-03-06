﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using System.Reflection;
using System.Web.Mvc.Routing;
using Blog.Presentation.Framework.Services.Controller;

namespace Blog.Presentation.Web.Controllers
{

    public class HomeController : Controller
    {

        private IWebHelper WebHelper { get { return new WebHelper(HttpContext); } }
        private IRegionService RegionService { get { return EngineContext.Current.Resolve<IRegionService>(); } }

        public ActionResult Index()
        {
            var list = RegionService.GetAdminMenus(0);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("CommonHelper.MapPath:" + CommonHelper.MapPath("~"));
            sb.AppendLine("WebHelper.GetCurrentIpAddress:" + WebHelper.GetCurrentIpAddress());
            sb.AppendLine("WebHelper.GetThisPageUrl:" + WebHelper.GetThisPageUrl(true));
            sb.AppendLine("WebHelper.IsCurrentConnectionSecured:" + WebHelper.IsCurrentConnectionSecured());
            sb.AppendLine("WebHelper.GetHost:" + WebHelper.GetHost(WebHelper.IsCurrentConnectionSecured()));
            sb.AppendLine("WebHelper.GetLocation:" + WebHelper.GetLocation());

            var routeData = new Dictionary<string, object>
            {
                {"area", "Admin"},
                {"controller", "Home"},
                {"action", "Index"}
            };

            var virtualPathData = RouteTable.Routes.GetVirtualPathForArea(HttpContext.Request.RequestContext, new RouteValueDictionary(routeData));

            var controllerNamespaces = virtualPathData.DataTokens["Namespaces"] as string[];
            if (controllerNamespaces == null)
                throw new Exception("未找到对应路由绑定的控制器命名空间");

            for (int i = 0; i < controllerNamespaces.Length; i++)
            {
                var controllerClassName = string.Format($"{controllerNamespaces[i]}.HomeController");
                Type controllerClassType = null;
                foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
                {
                    if (assembly.GetType(controllerClassName) != null)
                        controllerClassType = assembly.GetType(controllerClassName);
                }
            }

            return Content(sb.ToString());
        }

    }

}