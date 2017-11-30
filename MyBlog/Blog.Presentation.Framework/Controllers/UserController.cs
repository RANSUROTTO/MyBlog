using System;
using System.Web.Mvc.Filters;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Presentation.Framework.Controllers
{

    public class UserController : PublicController
    {

        #region fields

        private readonly ICustomer _authenticationCustomer;

        #endregion

        #region Constructor

        public UserController(IWorkContext workContext) : base(workContext)
        {
            _authenticationCustomer = workContext.Customer;
        }

        #endregion

        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            if (_authenticationCustomer == null)
            {
                Response.RedirectToRoute("Customer_Login", new { returnUrl = Request.Url?.ToString() });
                Response.End();
                return;
            }

            base.OnAuthentication(filterContext);
        }

    }

}
