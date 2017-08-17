﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void Passess_IsStaticResource_Success()
        {
        }



    }

}
