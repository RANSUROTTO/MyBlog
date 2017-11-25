using System.Web.Mvc;
using System.Web.Routing;
using Blog.Libraries.Core.Infrastructure;
using Blog.Presentation.Framework.Temporary.Route;

namespace Blog.Presentation.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("favicon.ico");
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Web_Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Blog.Presentation.Web.Controllers" });

            var routePublisher = EngineContext.Current.Resolve<IRoutePublisher>();
            routePublisher.RegisterRoutes(routes);
        }

    }
}
