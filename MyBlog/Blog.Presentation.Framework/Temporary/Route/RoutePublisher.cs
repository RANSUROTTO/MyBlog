using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Blog.Libraries.Core.Infrastructure.TypeFinder;

namespace Blog.Presentation.Framework.Temporary.Route
{

    public class RoutePublisher : IRoutePublisher
    {

        #region Field
        protected readonly ITypeFinder typeFinder;
        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        #endregion

        public void RegisterRoutes(RouteCollection routes)
        {
            var routeProviderTypes = typeFinder.FindClassesOfType<IRouteProvider>();
            var routeProviders = new List<IRouteProvider>();
            foreach (var providerType in routeProviderTypes)
            {
                var provider = Activator.CreateInstance(providerType) as IRouteProvider;
                routeProviders.Add(provider);
            }
            routeProviders = routeProviders.OrderByDescending(rp => rp.Priority).ToList();
            routeProviders.ForEach(rp => rp.RegisterRoutes(routes));
        }
    }

}
