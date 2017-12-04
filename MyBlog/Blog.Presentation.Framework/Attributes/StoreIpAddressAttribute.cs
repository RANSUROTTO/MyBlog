using System;
using System.Web.Mvc;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Services.Members;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Presentation.Framework.Attributes
{

    /// <summary>
    /// 代表一个用于更新用户最后访问的过滤器
    /// </summary>
    public class StoreIpAddressAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!DataSettingsHelper.DatabaseInstalled())
                return;

            if (filterContext?.HttpContext?.Request == null)
                return;

            //不要将筛选器应用于子请求
            if (filterContext.IsChildAction)
                return;

            //筛选接受Get请求
            if (!string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
                return;

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();

            //更新ip地址
            string currentIpAddress = webHelper.GetCurrentIpAddress();
            if (!string.IsNullOrEmpty(currentIpAddress))
            {
                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                var customer = workContext.Customer as Customer;
                if (customer != null && !currentIpAddress.Equals(customer.LastIpAddress, StringComparison.InvariantCultureIgnoreCase))
                {
                    var customerService = EngineContext.Current.Resolve<ICustomerService>();
                    customer.LastIpAddress = currentIpAddress;
                    customerService.UpdateCustomer(customer);
                }
            }
        }

    }

}
