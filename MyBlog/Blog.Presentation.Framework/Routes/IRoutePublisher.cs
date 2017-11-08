﻿using System.Web.Routing;

namespace Blog.Presentation.Framework.Routes
{

    /// <summary>
    /// 路由发布器接口
    /// </summary>
    public interface IRoutePublisher
    {

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">路由列表</param>
        void RegisterRoutes(RouteCollection routes);

    }

}