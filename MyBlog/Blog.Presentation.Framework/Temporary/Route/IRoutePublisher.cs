using System.Web.Routing;

namespace Blog.Presentation.Framework.Temporary.Route
{

    /// <summary>
    /// ·�ɷ�����
    /// </summary>
    public interface IRoutePublisher
    {


        /// <summary>
        /// ע��·��
        /// </summary>
        void RegisterRoutes(RouteCollection routes);

    }

}
