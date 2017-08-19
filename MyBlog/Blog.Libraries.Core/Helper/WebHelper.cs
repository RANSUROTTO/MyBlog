using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
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

        #region Methods

        public virtual string GetUrlReferrer()
        {
            string referrerUrl = string.Empty;
            if (IsRequestAvailable(_httpContext) && _httpContext.Request.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.PathAndQuery;

            return referrerUrl;
        }

        public virtual string GetCurrentIpAddress()
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            var result = "";
            try
            {
                if (_httpContext.Request.Headers != null)
                {
                    //X-Forwarded-For（XFF）HTTP请求头是一个事实上的标准
                    //用于识别客户端的IP地址
                    //通过HTTP代理或负载平衡器连接到Web服务器
                    var forwardedHttpHeader = "X-FORWARDED-FOR";
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ForwardedHTTPheader"]))
                    {
                        //但在某些情况下，服务器使用其他HTTP头
                        //在这种情况下，管理员可以指定自定义的转发HTTP标头
                        //例 CF-Connecting-IP, X-FORWARDED-PROTO 等
                        forwardedHttpHeader = ConfigurationManager.AppSettings["ForwardedHTTPheader"];
                    }

                    //它用于识别连接到Web服务器的客户端的IP地址
                    //通过HTTP代理或负载平衡器
                    string xff = _httpContext.Request.Headers.AllKeys
                        .Where(x => forwardedHttpHeader.Equals(x, StringComparison.OrdinalIgnoreCase))
                        .Select(k => _httpContext.Request.Headers[k])
                        .FirstOrDefault();

                    //如果要排除私有IP地址,请查阅
                    //http://stackoverflow.com/questions/2577496/how-can-i-get-the-clients-ip-address-in-asp-net-mvc
                    if (!string.IsNullOrEmpty(xff))
                    {
                        string lastIp = xff.Split(',').FirstOrDefault();
                        result = lastIp;
                    }
                }
            }
            catch (Exception)
            {
                return result;
            }

            //验证
            if (result == "::1")
                result = "127.0.0.1";

            //删除端口号
            if (!string.IsNullOrEmpty(result))
            {
                int index = result.IndexOf(":", StringComparison.InvariantCultureIgnoreCase);
                if (index != -1)
                    result = result.Substring(0, index);
            }

            return result;
        }

        public virtual string GetThisPageUrl(bool includeQueryString)
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetThisPageUrl(includeQueryString, useSsl);
        }

        public virtual string GetThisPageUrl(bool includeQueryString, bool useSsl)
        {
            if (!IsRequestAvailable(_httpContext))
                return string.Empty;

            //让主机考虑使用SSL
            var url = GetStoreHost(useSsl).TrimEnd('/');

            //获取或不添加查询字符串的完整URL
            url += includeQueryString ? _httpContext.Request.RawUrl : _httpContext.Request.Path;

            return url.ToLowerInvariant();
        }

        public virtual bool IsCurrentConnectionSecured()
        {
            bool useSsl = false;
            if (IsRequestAvailable(_httpContext))
            {
                //when your hosting uses a load balancer on their server then the Request.IsSecureConnection is never got set to true

                //1.使用 HTTP_CLUSTER_HTTPS
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Use_HTTP_CLUSTER_HTTPS"]) &&
                    Convert.ToBoolean(ConfigurationManager.AppSettings["Use_HTTP_CLUSTER_HTTPS"]))
                {
                    useSsl = ServerVariables("HTTP_CLUSTER_HTTPS") == "on";
                }
                //2.使用 HTTP_X_FORWARDED_PROTO
                else if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Use_HTTP_X_FORWARDED_PROTO"]) &&
                   Convert.ToBoolean(ConfigurationManager.AppSettings["Use_HTTP_X_FORWARDED_PROTO"]))
                {
                    useSsl = string.Equals(ServerVariables("HTTP_X_FORWARDED_PROTO"), "https", StringComparison.OrdinalIgnoreCase);
                }
                else
                {
                    useSsl = _httpContext.Request.IsSecureConnection;
                }

            }

            return useSsl;
        }

        public virtual string ServerVariables(string name)
        {
            string result = string.Empty;

            try
            {
                if (!IsRequestAvailable(_httpContext))
                    return result;

                if (_httpContext.Request.ServerVariables[name] != null)
                {
                    result = _httpContext.Request.ServerVariables[name];
                }
            }
            catch
            {
                result = string.Empty;
            }
            return result;
        }

        public virtual string GetStoreHost(bool useSsl)
        {
            throw new NotImplementedException();
            var result = "";
            var httpHost = ServerVariables("HTTP_HOST");
            if (!string.IsNullOrEmpty(httpHost))
            {
                result = "http://" + httpHost;
                if (!result.EndsWith("/"))
                    result += "/";
            }

            /*if (DataSettingsHelper.DatabaseInstalled())
            {
                #region Database is installed

                //let's resolve IWorkContext  here.
                //Do not inject it via constructor  because it'll cause circular references
                var storeContext = EngineContext.Current.Resolve<IStoreContext>();
                var currentStore = storeContext.CurrentStore;
                if (currentStore == null)
                    throw new Exception("Current store cannot be loaded");

                if (String.IsNullOrWhiteSpace(httpHost))
                {
                    //HTTP_HOST variable is not available.
                    //This scenario is possible only when HttpContext is not available (for example, running in a schedule task)
                    //in this case use URL of a store entity configured in admin area
                    result = currentStore.Url;
                    if (!result.EndsWith("/"))
                        result += "/";
                }

                if (useSsl)
                {
                    result = !String.IsNullOrWhiteSpace(currentStore.SecureUrl) ?
                        //Secure URL specified. 
                        //So a store owner don't want it to be detected automatically.
                        //In this case let's use the specified secure URL
                        currentStore.SecureUrl :
                        //Secure URL is not specified.
                        //So a store owner wants it to be detected automatically.
                        result.Replace("http:/", "https:/");
                }
                else
                {
                    if (currentStore.SslEnabled && !String.IsNullOrWhiteSpace(currentStore.SecureUrl))
                    {
                        //SSL is enabled in this store and secure URL is specified.
                        //So a store owner don't want it to be detected automatically.
                        //In this case let's use the specified non-secure URL
                        result = currentStore.Url;
                    }
                }
                #endregion
            }
            else
            {
                #region Database is not installed
                if (useSsl)
                {
                    //Secure URL is not specified.
                    //So a store owner wants it to be detected automatically.
                    result = result.Replace("http:/", "https:/");
                }
                #endregion
            }*/

            if (!result.EndsWith("/"))
                result += "/";
            return result.ToLowerInvariant();
        }

        public virtual string GetStoreLocation()
        {
            bool useSsl = IsCurrentConnectionSecured();
            return GetStoreLocation(useSsl);
        }

        public virtual string GetStoreLocation(bool useSsl)
        {
            string result = GetStoreHost(useSsl);
            if (result.EndsWith("/"))
                result = result.Substring(0, result.Length - 1);
            if (IsRequestAvailable(_httpContext))
                result = result + _httpContext.Request.ApplicationPath;
            if (!result.EndsWith("/"))
                result += "/";

            return result.ToLowerInvariant();
        }

        public virtual bool IsStaticResource(HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            string path = request.Path;
            string extension = VirtualPathUtility.GetExtension(path);

            if (extension == null) return false;

            return _staticFileExtensions.Contains(extension);
        }

        public virtual string ModifyQueryString(string url, string queryStringModification, string anchor)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryStringModification == null)
                queryStringModification = string.Empty;
            queryStringModification = queryStringModification.ToLowerInvariant();

            if (anchor == null)
                anchor = string.Empty;
            anchor = anchor.ToLowerInvariant();

            string str = string.Empty;
            string str2 = string.Empty;
            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("#", StringComparison.Ordinal));
            }

            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("?", StringComparison.Ordinal));
            }

            if (!string.IsNullOrEmpty(queryStringModification))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split('&'))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split('=');
                            if (strArray.Length == 2)
                            {
                                if (!dictionary.ContainsKey(strArray[0]))
                                {
                                    //如果它已经存在,不进行覆盖
                                    //两个相同的查询参数,理论上是不可能的
                                    //但是MVC对复选框有一些较差的实现，我们可以有两个值
                                    //在这里找到更多信息: http://www.mindstorminteractive.com/topics/jquery-fix-asp-net-mvc-checkbox-truefalse-value/
                                    //我们做这个验证只是为了确保第一个不被覆盖
                                    dictionary[strArray[0]] = strArray[1];
                                }
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    foreach (string str4 in queryStringModification.Split('&'))
                    {
                        if (!string.IsNullOrEmpty(str4))
                        {
                            string[] strArray2 = str4.Split('=');
                            if (strArray2.Length == 2)
                            {
                                dictionary[strArray2[0]] = strArray2[1];
                            }
                            else
                            {
                                dictionary[str4] = null;
                            }
                        }
                    }
                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
                else
                {
                    str = queryStringModification;
                }
            }

            if (!string.IsNullOrEmpty(anchor))
            {
                str2 = anchor;
            }

            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2))).ToLowerInvariant();

        }

        public virtual string RemoveQueryString(string url, string queryString)
        {
            if (url == null)
                url = string.Empty;
            url = url.ToLowerInvariant();

            if (queryString == null)
                queryString = string.Empty;
            queryString = queryString.ToLowerInvariant();

            string str = string.Empty;
            string str2 = string.Empty;

            if (url.Contains("#"))
            {
                str2 = url.Substring(url.IndexOf("#", StringComparison.Ordinal) + 1);
                url = url.Substring(0, url.IndexOf("#", StringComparison.Ordinal));
            }
            if (url.Contains("?"))
            {
                str = url.Substring(url.IndexOf("?") + 1);
                url = url.Substring(0, url.IndexOf("?"));
            }

            if (!string.IsNullOrEmpty(queryString))
            {
                if (!string.IsNullOrEmpty(str))
                {
                    var dictionary = new Dictionary<string, string>();
                    foreach (string str3 in str.Split('&'))
                    {
                        if (!string.IsNullOrEmpty(str3))
                        {
                            string[] strArray = str3.Split('=');
                            if (strArray.Length == 2)
                            {
                                dictionary[strArray[0]] = strArray[1];
                            }
                            else
                            {
                                dictionary[str3] = null;
                            }
                        }
                    }
                    dictionary.Remove(queryString);

                    var builder = new StringBuilder();
                    foreach (string str5 in dictionary.Keys)
                    {
                        if (builder.Length > 0)
                        {
                            builder.Append("&");
                        }
                        builder.Append(str5);
                        if (dictionary[str5] != null)
                        {
                            builder.Append("=");
                            builder.Append(dictionary[str5]);
                        }
                    }
                    str = builder.ToString();
                }
            }

            return (url + (string.IsNullOrEmpty(str) ? "" : ("?" + str)) + (string.IsNullOrEmpty(str2) ? "" : ("#" + str2)));
        }

        public virtual T QueryString<T>(string name)
        {
            string queryParam = null;
            if (IsRequestAvailable(_httpContext) && _httpContext.Request.QueryString[name] != null)
                queryParam = _httpContext.Request.QueryString[name];

            if (!string.IsNullOrEmpty(queryParam))
                return CommonHelper.To<T>(queryParam);

            return default(T);
        }

        public virtual void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "")
        {
            throw new NotImplementedException();
        }

        public virtual bool IsRequestBeingRedirected
        {
            get
            {
                var response = _httpContext.Response;
                return response.IsRequestBeingRedirected;
            }
        }

        public virtual bool IsPostBeingDone
        {
            get
            {
                if (_httpContext.Items["IsPOSTBeingDone"] == null)
                    return false;
                return Convert.ToBoolean(_httpContext.Items["IsPOSTBeingDone"]);
            }
            set
            {
                _httpContext.Items["IsPOSTBeingDone"] = value;
            }
        }

        #endregion

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
