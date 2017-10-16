using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Libraries.Services.Authentication;
using Blog.Presentation.Web.Models.Customer;

namespace Blog.Presentation.Web.Controllers
{
    public class CustomerController : Controller
    {

        #region Fields

        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructor

        public CustomerController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        #endregion

        #region Login / Logout

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CustomerLoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {

            }
            return View(model);
        }


        #endregion

    }
}