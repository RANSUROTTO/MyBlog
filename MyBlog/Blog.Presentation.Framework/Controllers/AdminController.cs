using System;
using System.Web.Mvc.Filters;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Presentation.Framework.Controllers
{

    public class AdminController : PublicController
    {

        #region Fields

        private readonly IAdmin _authenticationAdmin;

        #endregion

        #region Constructor

        public AdminController(IWorkContext workContext) : base(workContext)
        {
            _authenticationAdmin = workContext.Admin;
        }

        #endregion

        #region Methods

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_authenticationAdmin == null)
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
