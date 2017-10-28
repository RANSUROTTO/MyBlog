using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Libraries.Core.Domain.Members;
using Blog.Libraries.Data.Domain.Members;
using Blog.Libraries.Data.Domain.Members.Enum;
using Blog.Libraries.Services.Authentication;
using Blog.Libraries.Services.Members;
using Blog.Presentation.Web.Models.Customer;

namespace Blog.Presentation.Web.Controllers
{
    public class CustomerController : Controller
    {
        #region Fields

        private readonly IAuthenticationService _authenticationService;
        private readonly ICustomerService _customerService;

        #endregion

        #region Constructor

        public CustomerController(IAuthenticationService authenticationService, ICustomerService customerService)
        {
            _authenticationService = authenticationService;
            _customerService = customerService;
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
                var loginResult = _customerService.ValidateCustomer(model.UsernameOrEmail, model.Password);

                switch (loginResult)
                {
                    case CustomerLoginResult.Successful:
                        //Sign In
                        var customer = _customerService.GetCustomerByUsernameOrEmail(model.UsernameOrEmail);
                        _authenticationService.SignIn(customer, createPersistentCookie: model.RememberMe);
                        return RedirectToRoute("HomePage");

                    case CustomerLoginResult.CustomerNotExist:
                        ModelState.AddModelError("", "Customer Not Exist");
                        break;

                    case CustomerLoginResult.LockedOut:
                        ModelState.AddModelError("", "Customer LockedOut");
                        break;

                    case CustomerLoginResult.WrongPassword:
                        ModelState.AddModelError("", "Password Wrong");
                        break;
                }
            }
            return View(model);
        }


        #endregion

    }
}