using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Blog.Presentation.Framework.Localization
{
    public static class LocalizedRouteExtensions
    {

        #region MapRoute

        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url)
        {
            return MapLocalizedRoute(routes, name, url, null /* defaults */, (object)null /* constraints */);
        }
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults)
        {
            return MapLocalizedRoute(routes, name, url, defaults, (object)null /* constraints */);
        }
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints)
        {
            return MapLocalizedRoute(routes, name, url, defaults, constraints, null /* namespaces */);
        }
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, string[] namespaces)
        {
            return MapLocalizedRoute(routes, name, url, null /* defaults */, null /* constraints */, namespaces);
        }
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, string[] namespaces)
        {
            return MapLocalizedRoute(routes, name, url, defaults, null /* constraints */, namespaces);
        }

        /// <summary>
        /// 扩展RouteCollection提供映射LocalizedRoute路由的方法
        /// </summary>
        /// <param name="routes">RouteCollection实例</param>
        /// <param name="name">路由名称</param>
        /// <param name="url">路由URL模式</param>
        /// <param name="defaults">路由默认参数</param>
        /// <param name="constraints">路由约束</param>
        /// <param name="namespaces">路由自定义值</param>
        /// <returns>Route对象</returns>
        public static Route MapLocalizedRoute(this RouteCollection routes, string name, string url, object defaults, object constraints, string[] namespaces)
        {
            if (routes == null)
            {
                throw new ArgumentNullException("routes");
            }
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            var route = new LocalizedRoute(url, new MvcRouteHandler())
            {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints),
                DataTokens = new RouteValueDictionary()
            };

            if ((namespaces != null) && (namespaces.Length > 0))
            {
                route.DataTokens["Namespaces"] = namespaces;
            }

            //将生成的Route实例添加至RouteCollection实例中
            routes.Add(name, route);

            return route;
        }

        #endregion

        /// <summary>
        /// 清除路由集合内每一个为本地化路由实例的_seoFriendlyUrlsForLanguagesEnabled字段
        /// </summary>
        /// <param name="routes"></param>
        public static void ClearSeoFriendlyUrlsCachedValueForRoutes(this RouteCollection routes)
        {
            if (routes == null)
                throw new ArgumentNullException("routes");
            foreach (var route in routes)
            {
                if (route is LocalizedRoute)
                {
                    ((LocalizedRoute)route).ClearSeoFriendlyUrlsCachedValue();
                }
            }
        }

    }
}
