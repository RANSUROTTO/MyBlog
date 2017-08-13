using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Libraries.Core.Helper
{
    public class WebHelper : IWebHelper
    {

        #region Field

        /// <summary>
        /// http上下文
        /// </summary>
        private readonly HttpContextBase _httpContext;

        /// <summary>
        /// 需要引擎不进行处理的静态文件
        /// </summary>
        private readonly string[] _staticFileExtensions;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public WebHelper(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
            this._staticFileExtensions = new[] { ".axd", ".ashx", ".bmp", ".css", ".gif", ".htm", ".html", ".ico", ".jpeg", ".jpg", ".js", ".png", ".rar", ".zip" };
        }

        #endregion

        public string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;
            if (IsRequestAvailable(_httpContext) && _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }

        public string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            var result = "";
            try
            {
                if (_httpContext.Request.Headers != null)
                {

                }
            }
            catch (Exception)
            {

                throw;
            }



            throw new NotImplementedException();
        }

        public string GetThisPageUrl(bool includeQueryString)
        {
            throw new NotImplementedException();
        }

        public string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            throw new NotImplementedException();
        }

        public bool IsCurrentConnectionSecured()
        {
            throw new NotImplementedException();
        }

        public string ServerVariables(string name)
        {
            throw new NotImplementedException();
        }

        public string GetStoreHost(bool useSsl)
        {
            throw new NotImplementedException();
        }

        public string GetStoreLocation()
        {
            throw new NotImplementedException();
        }

        public string GetStoreLocation(bool useSsl)
        {
            throw new NotImplementedException();
        }

        public bool IsStaticResource(HttpRequest request)
        {
            throw new NotImplementedException();
        }

        public string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            throw new NotImplementedException();
        }

        public string RemoveQueryString(string url, string queryString)
        {
            throw new NotImplementedException();
        }

        public T QueryString<T>(string name)
        {
            throw new NotImplementedException();
        }

        public void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "")
        {
            throw new NotImplementedException();
        }

        public bool IsRequestBeingRedirected
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsPostBeingDone
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        #region Utilities

        /// <summary>
        /// 检查httpcontext对象请求是否可用
        /// </summary>
        protected virtual bool IsRequestAvailable(HttpContextBase httpContext)
        {
            if (httpContext == null)
                return false;
            try
            {
                if (httpContext.Request == null)
                    return false;
            }
            catch (HttpException)
            {
                return false;
            }
            return true;
        }

        #endregion



    }
}
