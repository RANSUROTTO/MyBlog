using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Blog.Presentation.Framework.CommonModel.ActionResult
{

    /// <summary>
    /// JsonConvert Ч�ʸ���
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
                throw new InvalidOperationException("��ǰ������ֹGet����JSON����");
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
