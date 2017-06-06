using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Presentation.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //禁用 "X-AspNetMvc-Version" 请求头
            MvcHandler.DisableMvcResponseHeader = true;

            //清除默认视图引擎
            ViewEngines.Engines.Clear();
            //仅适用Razor视图引擎
            ViewEngines.Engines.Add(new RazorViewEngine());


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }


        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            //禁用 "Server" 请求头
            app?.Context?.Response.Headers.Remove("Server");
        }

    }
}
