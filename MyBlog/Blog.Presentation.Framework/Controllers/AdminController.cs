using System;
using System.Web.Mvc.Filters;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Domain.Members;
using Blog.Libraries.Data.Domain.Member;

namespace Blog.Presentation.Framework.Controllers
{

    public class AdminController : PublicController
    {

        #region Fields

        public Admin AuthenticationAdmin { get; }

        #endregion

        #region Constructor

        public AdminController(IWorkContext workContext) : base(workContext)
        {
            AuthenticationAdmin = workContext.Admin as Admin;
        }

        #endregion

        #region Methods

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (AuthenticationAdmin == null)
            {
                Response.RedirectToRoute("login", new { returnUrl = Request.Url?.ToString() });
                Response.End();
                return;
            }

            base.OnAuthentication(filterContext);
        }

        #endregion

    }

}
