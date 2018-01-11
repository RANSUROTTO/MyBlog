using System.IO;
using System.Text;
using System.Web.Mvc;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.CommonModel.ActionResult;

namespace Blog.Presentation.Framework.Controllers
{

    /// <summary>
    /// 基础控制器
    /// StoreIpAddress:存储用户最后访问的站点的客户端地址
    /// </summary>
    [StoreIpAddress]
    public abstract class BaseController : AsyncController
    {

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        protected BaseController()
        {
        }

        #endregion

        #region Methods

        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        /// <summary>
        /// 读取分布视图为html字符串
        /// </summary>
        /// <param name="viewName">视图名称</param>
        /// <param name="model">视图模型</param>
        /// <returns>html字符串</returns>
        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            //获取视图名称
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            //设置视图模型
            this.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                //读取生成的视图
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, sw);
                viewResult.View.Render(viewContext, sw);
                //返回视图字符串
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion

        #region Utilities

        public NetJsonResult NetJson(object data)
        {
            return NetJson(data, JsonRequestBehavior.AllowGet);
        }

        public NetJsonResult NetJson(object data, JsonRequestBehavior behavior)
        {
            return NetJson(data, null, null, JsonRequestBehavior.AllowGet);
        }

        public NetJsonResult NetJson(object data, string contentType)
        {
            return NetJson(data, contentType, null, JsonRequestBehavior.AllowGet);
        }

        public NetJsonResult NetJson(object data, string contentType, JsonRequestBehavior behavior)
        {
            return NetJson(data, contentType, null, behavior);
        }

        public NetJsonResult NetJson(object data, string contentType, Encoding contentEncoding)
        {
            return NetJson(data, contentType, contentEncoding, JsonRequestBehavior.AllowGet);
        }

        public virtual NetJsonResult NetJson(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new NetJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }

        #endregion

    }

}
