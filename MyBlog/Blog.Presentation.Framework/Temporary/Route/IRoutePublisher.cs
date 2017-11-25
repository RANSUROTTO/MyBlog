using System.Web.Routing;

namespace Blog.Presentation.Framework.Temporary.Route
{

    /// <summary>
    /// 路由发布器
    /// </summary>
    public interface IRoutePublisher
    {


        /// <summary>
        /// 注册路由
        /// </summary>
        void RegisterRoutes(RouteCollection routes);

    }

}
