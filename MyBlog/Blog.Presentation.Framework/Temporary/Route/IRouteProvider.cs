using System.Web.Routing;

namespace Blog.Presentation.Framework.Temporary.Route
{

    public interface IRouteProvider
    {

        void RegisterRoutes(RouteCollection routes);

        int Priority { get; }

    }

}
