using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Blog.Presentation.Framework.CommonModel.ActionResult
{

    /// <summary>
    /// JsonConvert 效率更高
    /// </summary>
    public class NetJsonResult : JsonResult
    {

        public NetJsonResult()
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("当前设置阻止Get请求JSON数据");
            }
            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                response.Write(JsonConvert.SerializeObject(Data));
            }
        }

    }

}
