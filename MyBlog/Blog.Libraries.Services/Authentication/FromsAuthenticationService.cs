using System;
using System.Web;
using System.Web.Security;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using Blog.Libraries.Services.Members;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Services.Authentication
{

    /// <summary>
    /// 身份验证业务实现
    /// </summary>
    public class FromsAuthenticationService : IAuthenticationService
    {

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly IAdminService _adminService;
        private readonly IGuestService _guestService;
        private readonly TimeSpan _expirationTimeSpan;
        private readonly CustomerSettings _customerSettings;


        private Guest _cacheGuest;
        private Customer _cacheCustomer;
        private Admin _cacheAdmin;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public FromsAuthenticationService(HttpContextBase httpContext
            , ICustomerService customerService
            , IAdminService adminService
            , IGuestService guestService
            , TimeSpan expirationTimeSpan
            , CustomerSettings customerSettings)
        {
            _httpContext = httpContext;
            _customerService = customerService;
            _adminService = adminService;
            _guestService = guestService;
            _expirationTimeSpan = expirationTimeSpan;
            _customerSettings = customerSettings;
        }

        #endregion

        #region Methods

        public virtual void SignIn(Guid guid, AuthenticationType type, DateTime? expirantion = null, bool createPersistentCookie = true)
        {
            var now = DateTime.UtcNow.ToLocalTime();
            var ticketName = _customerSettings.AuthenticationTicketName;
            var actualExpirationTimeSpan = expirantion.HasValue ?
                (expirantion.Value - now)
                : _expirationTimeSpan;

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                ticketName,
                now,
                now.Add(actualExpirationTimeSpan),
                createPersistentCookie,
                guid.ToString(),
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = _httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(FormsAuthentication.FormsCookieName) { HttpOnly = true };
                if (FormsAuthentication.CookieDomain != null)
                {
                    cookie.Domain = FormsAuthentication.CookieDomain;
                }
                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                cookie.Secure = FormsAuthentication.RequireSSL;
                cookie.Path = FormsAuthentication.FormsCookiePath;
            }

            var ticketDictionary = StringToAuthenticationTicket(cookie.Value);
            ticketDictionary[type.ToString().ToLower(CultureInfo.InvariantCulture)] = encryptedTicket;
            cookie.Value = AuthenticationTicketToString(ticketDictionary);

            _httpContext.Response.Cookies.Set(cookie);
        }

        public virtual void SignOut(Guid guid, AuthenticationType type)
        {
            throw new NotImplementedException();
        }

        public virtual T GetAuthenticationMember<T>() where T : class
        {
            var cacheMember = GetCacheMemberByMemberType<T>();
            if (cacheMember != null)
                return (T)cacheMember;

            if (_httpContext?.Request == null
                || !_httpContext.Request.IsAuthenticated
                || !(_httpContext.User.Identity is FormsIdentity))
                return null;

            var authenticationType = GetAuthenticationTypeByMemberType<T>();

            var fromsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var ticketDictionary = StringToAuthenticationTicket(fromsIdentity.Ticket.UserData);

            T t = (T)GetMemberByAuthenticationTypeAndTicket(authenticationType, ticketDictionary);

            return t;
        }

        #endregion

        #region Utilities

        private object GetMemberByAuthenticationTypeAndTicket(AuthenticationType authenticationType, Dictionary<string, string> ticketDictionary)
        {
            switch (authenticationType)
            {
                case AuthenticationType.Guest:
                    break;
                case AuthenticationType.Admin:
                    break;
                case AuthenticationType.Customer:
                    break;
            }
            return null;
        }

        private object GetCacheMemberByMemberType<T>()
        {
            if (typeof(T) == typeof(Guest))
                return _cacheGuest;
            if (typeof(T) == typeof(Admin))
                return _cacheAdmin;
            if (typeof(T) == typeof(Customer))
                return _cacheCustomer;

            return null;
        }

        private AuthenticationType GetAuthenticationTypeByMemberType<T>()
        {
            if (typeof(T) == typeof(Guest))
                return AuthenticationType.Guest;
            if (typeof(T) == typeof(Admin))
                return AuthenticationType.Admin;
            if (typeof(T) == typeof(Customer))
                return AuthenticationType.Customer;

            throw new ArgumentException(string.Format("Unsupported Member Type {0}", typeof(T).Name));
        }

        private string AuthenticationTicketToString(Dictionary<string, string> ticketDictionary)
        {
            var dictionaryTypeConverter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            return (string)dictionaryTypeConverter.ConvertTo(ticketDictionary, typeof(string));
        }

        private Dictionary<string, string> StringToAuthenticationTicket(string ticketString)
        {
            var dictionaryTypeConverter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            return (Dictionary<string, string>)dictionaryTypeConverter.ConvertFrom(ticketString) ?? new Dictionary<string, string>();
        }

        #endregion

    }

}
