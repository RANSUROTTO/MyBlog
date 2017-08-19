using System;
using System.Collections.Specialized;
using Blog.Libraries.Core.Helper;
using System.Web;
using Blog.Libraries.Core.Fakes;
using NUnit.Framework;
using Blog.Tests;

namespace Blog.Libraries.Core.Tests.Helper
{

    [TestFixture]
    public class WebHelperTests
    {

        private HttpContextBase _httpContext;
        private IWebHelper _webHelper;

        [Test]
        public void Passes_GetUrlReferrer_Success()
        {
            var uri = new Uri("http://www.example.com/request");
            var uriReferrer = new Uri("http://www.example.com/referrer?name=lancelot");
            var httpRequest = new FakeHttpRequest("~/request", uri, uriReferrer);
            _httpContext = new FakeHttpContext("~/");
            (_httpContext as FakeHttpContext)?.SetRequest(httpRequest);
            _webHelper = new WebHelper(_httpContext);
            //Tests
            var result = _webHelper.GetUrlReferrer();
            result.TestEqual("/referrer?name=lancelot");
        }

        [Test]
        public void Passes_ServerVariables_Get_Success()
        {
            var serverVariables = new NameValueCollection();
            serverVariables.Add("key1", "value1");
            serverVariables.Add("key2", "value2");
            _httpContext = new FakeHttpContext("~/", "Get", null, null, null, null, null, serverVariables);
            _webHelper = new WebHelper(_httpContext);
            //Tests
            _webHelper.ServerVariables("key1").TestEqual("value1");
            _webHelper.ServerVariables("key2").TestEqual("value2");
            _webHelper.ServerVariables("key3").TestEqual(string.Empty);
        }

        [Test]
        public void Passess_ModifyQueryString_Success()
        {
            string url = "/Request?parameter=123#nav1";
            HttpContextBase httpContext = new FakeHttpContext("~/");
            _webHelper = new WebHelper(httpContext);

            //不覆盖
            _webHelper.ModifyQueryString(url, "property=abc", "nva2").ToLower()
                .TestEqual("/Request?parameter=123&property=abc#nva2".ToLower());

            //覆盖
            _webHelper.ModifyQueryString(url, "property=abc&parameter=456", "nva2").ToLower()
                .TestEqual("/Request?parameter=456&property=abc#nva2".ToLower());
        }

        [Test]
        public void Passess_RemoveQueryString_Success()
        {
            string url = "/Request?parameter=123#nav1";
            HttpContextBase httpContext = new FakeHttpContext("~/");
            _webHelper = new WebHelper(httpContext);

            _webHelper.RemoveQueryString(url, "parameter").TestEqual("/request#nav1".ToLower());

            url = "/Request?parameter=123&property=abc#nav1";
            _webHelper.RemoveQueryString(url, "parameter").TestEqual("/request?property=abc#nav1".ToLower());
        }




    }

}
