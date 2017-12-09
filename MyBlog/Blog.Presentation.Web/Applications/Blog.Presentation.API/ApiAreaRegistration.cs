using System.Web.Mvc;

namespace Blog.Presentation.API
{

    public class APIAreaRegistration : AreaRegistration
    {

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "API_Default",
                "API/{controller}/{action}/{id}",
                new { area = AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "Blog.Presentation.API.Controllers" }
                );
        }

        public override string AreaName => "API";

    }

}