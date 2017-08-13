using System.Web;

namespace Blog.Libraries.Core.Helper
{

    public interface IWebHelper
    {

        /// <summary>
        /// 获取引用url链接(AbsoloutePath and Query)
        /// </summary>
        /// <returns>url链接</returns>
        string GetUrlReferrer();

        /// <summary>
        /// 获取上下文ip地址
        /// </summary>
        /// <returns>url链接</returns>
        string GetCurrentIpAddress();

        /// <summary>
        /// 获取当前页面名称
        /// </summary>
        /// <param name="includeQueryString">指示是否包含查询字符串</param>
        /// <returns>页面名称</returns>
        string GetThisPageUrl(bool includeQueryString);

        /// <summary>
        /// 获取当前页面名称
        /// </summary>
        /// <param name="includeQueryString">指示是否包含查询字符串</param>
        /// <param name="useSsl">指示是否获得SSL保护的页面</param>
        /// <returns>页面名称</returns>
        string GetThisPageUrl(bool includeQueryString, bool useSsl);

        /// <summary>
        /// 获取一个值，指示当前连接是否被保护
        /// </summary>
        bool IsCurrentConnectionSecured();

        /// <summary>
        /// 通过名称获取服务器变量
        /// </summary>
        /// <param name="name">变量名称</param>
        /// <returns>服务器变量</returns>
        string ServerVariables(string name);

        /// <summary>
        /// 获取主机位置
        /// </summary>
        /// <param name="useSsl">使用ssl</param>
        /// <returns>主机位置</returns>
        string GetStoreHost(bool useSsl);

        /// <summary>
        /// 获取存储位置
        /// </summary>
        /// <returns>存储位置</returns>
        string GetStoreLocation();

        /// <summary>
        /// 获取存储位置
        /// </summary>
        /// <param name="useSsl">使用ssl</param>
        /// <returns>存储位置</returns>
        string GetStoreLocation(bool useSsl);

        /// <summary>
        /// 获取该请求是否为请求指定不需要处理的静态资源之一
        /// </summary>
        /// <param name="request">http请求</param>
        bool IsStaticResource(HttpRequest request);

        /// <summary>
        /// 修改查询字符串
        /// </summary>
        /// <param name="url">需要修改的url</param>
        /// <param name="queryStringModification">查询字符串修改</param>
        /// <param name="anchor">Anchor</param>
        /// <returns>修改后的url</returns>
        string ModifyQueryString(string url, string queryStringModification, string anchor);

        /// <summary>
        /// 从url中删除查询字符串
        /// </summary>
        /// <param name="url">需要修改的url</param>
        /// <param name="queryString">要删除的查询字符串</param>
        /// <returns>修改后的url</returns>
        string RemoveQueryString(string url, string queryString);

        /// <summary>
        /// 通过名称获取查询字符串值
        /// </summary>
        /// <param name="name">参数名称</param>
        /// <returns>查询字符串值</returns>
        T QueryString<T>(string name);

        /// <summary>
        /// 重新启动应用程序域
        /// </summary>
        /// <param name="makeRedirect">表示重启后是否应该重定向</param>
        /// <param name="redirectUrl">重定向网址;如果要重定向到当前页面URL,则为空字符串</param>
        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "");

        /// <summary>
        /// 获取一个值，该值指示客户端是否被重定向到新位置
        /// </summary>
        bool IsRequestBeingRedirected { get; }

        /// <summary>
        /// 获取或设置一个值，该值指示客户端是否使用POST重定向到新位置
        /// </summary>
        bool IsPostBeingDone { get; set; }

    }

}
