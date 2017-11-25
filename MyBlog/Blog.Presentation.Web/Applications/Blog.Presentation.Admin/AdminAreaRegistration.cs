using System.Web.Mvc;

namespace Blog.Presentation.Admin
{

    public class AdminAreaRegistration : AreaRegistration
    {
        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_Default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "index", id = UrlParameter.Optional },
                new[] { "Blog.Presentation.Admin.Controllers" }
                );

        }

        public override string AreaName => "Admin";
    }

}