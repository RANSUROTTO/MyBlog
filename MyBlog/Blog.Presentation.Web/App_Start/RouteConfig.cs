using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Presentation.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                name: "Web_Static_Default",
                url: "{controller}/{action}.html/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Blog.Presentation.Web.Controllers" });

            routes.MapRoute(
                name: "Web_Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "Blog.Presentation.Web.Controllers" });

        }
    }
}
