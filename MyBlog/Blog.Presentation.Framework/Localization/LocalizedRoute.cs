using System.Web;
using System.Web.Routing;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Data.Settings;

namespace Blog.Presentation.Framework.Localization
{

    /// <summary>
    /// 提供定义本地化路由的属性和方法，以及获取有关本地化路由的信息
    /// </summary>
    public class LocalizedRoute : Route
    {

        #region Fields

        private bool? _seoFriendlyUrlsForLanguagesEnabled;

        #endregion

        #region Properties

        /// <summary>
        /// 是否启用了具有多种语言的SEO友好URL
        /// </summary>
        protected bool SeoFriendlyUrlsForLanguagesEnabled
        {
            get
            {
                if (!_seoFriendlyUrlsForLanguagesEnabled.HasValue)
                    _seoFriendlyUrlsForLanguagesEnabled = EngineContext.Current.Resolve<LocalizationSettings>().SeoFriendlyUrlsForLanguagesEnabled;

                return _seoFriendlyUrlsForLanguagesEnabled.Value;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 指定url模式和处理程序类初始化该类新实例
        /// </summary>
        /// <param name="url">路由的url模式</param>
        /// <param name="routeHandler">路由的处理程序类</param>
        public LocalizedRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler)
        {
        }

        /// <summary>
        /// 指定url模式、默认参数值和处理程序类初始化该类新实例
        /// </summary>
        /// <param name="url">路由的url模式</param>
        /// <param name="defaults">默认的路由参数</param>
        /// <param name="routeHandler">路由的处理程序类</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler)
        {
        }

        /// <summary>
        /// 指定url模式、默认参数值、约束和处理程序类初始化该类新实例
        /// </summary>
        /// <param name="url">路由的url模式</param>
        /// <param name="defaults">默认的路由参数</param>
        /// <param name="constraints">路由的约束</param>
        /// <param name="routeHandler">路由的处理程序类</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler)
        {
        }

        /// <summary>
        /// 使用指定的URL模式、处理程序类、默认参数值、约束和自定义值初始化该类新实例
        /// </summary>
        /// <param name="url">路由的url模式</param>
        /// <param name="defaults">默认的路由参数</param>
        /// <param name="constraints">路由的约束</param>
        /// <param name="dataTokens">自定义值</param>
        /// <param name="routeHandler">路由的处理程序类</param>
        public LocalizedRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// 返回请求的路由信息
        /// </summary>
        /// <param name="httpContext">封装HTTP请求信息的对象</param>
        /// <returns>
        /// 包含路由定义中的值的对象
        /// </returns>
        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            if (DataSettingsHelper.DatabaseInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath;
                string applicationPath = httpContext.Request.ApplicationPath;
                if (virtualPath.IsLocalizedUrl(applicationPath, false))
                {
                    //In ASP.NET Development Server, an URL like "http://localhost/Blog.aspx/Categories/BabyFrog" will return 
                    //"~/Blog.aspx/Categories/BabyFrog" as AppRelativeCurrentExecutionFilePath.
                    //However, in II6, the AppRelativeCurrentExecutionFilePath is "~/Blog.aspx"
                    //It seems that IIS6 think we're process Blog.aspx page.
                    //So, I'll use RawUrl to re-create an AppRelativeCurrentExecutionFilePath like ASP.NET Development Server.

                    //Question: should we do path rewriting right here?
                    string rawUrl = httpContext.Request.RawUrl;
                    var newVirtualPath = rawUrl.RemoveLanguageSeoCodeFromRawUrl(applicationPath);
                    if (string.IsNullOrEmpty(newVirtualPath))
                        newVirtualPath = "/";
                    newVirtualPath = newVirtualPath.RemoveApplicationPathFromRawUrl(applicationPath);
                    newVirtualPath = "~" + newVirtualPath;
                    httpContext.RewritePath(newVirtualPath, true);
                }
            }
            RouteData data = base.GetRouteData(httpContext);
            return data;
        }

        /// <summary>
        /// 返回与路由相关联的URL的信息
        /// </summary>
        /// <param name="requestContext">封装请求路由信息的对象</param>
        /// <param name="values">包含的路由参数</param>
        /// <returns>
        /// 包含与路由相关联的URL信息的对象
        /// </returns>
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            VirtualPathData data = base.GetVirtualPath(requestContext, values);

            if (data != null && DataSettingsHelper.DatabaseInstalled() && this.SeoFriendlyUrlsForLanguagesEnabled)
            {
                string rawUrl = requestContext.HttpContext.Request.RawUrl;
                string applicationPath = requestContext.HttpContext.Request.ApplicationPath;
                if (rawUrl.IsLocalizedUrl(applicationPath, true))
                {
                    data.VirtualPath = string.Concat(rawUrl.GetLanguageSeoCodeFromUrl(applicationPath, true), "/",
                        data.VirtualPath);
                }
            }
            return data;
        }

        /// <summary>
        /// 清除 字段_seoFriendlyUrlsForLanguagesEnabled 值
        /// </summary>
        public virtual void ClearSeoFriendlyUrlsCachedValue()
        {
            _seoFriendlyUrlsForLanguagesEnabled = null;
        }

        #endregion

    }

}
