using System;
using System.Web;
using System.Web.Security;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using Blog.Libraries.Services.Members;
using Blog.Libraries.Data.Domain.Members;
using Blog.Libraries.Data.Settings;

namespace Blog.Libraries.Services.Authentication
{

    /// <summary>
    /// Froms身份验证业务实现
    /// </summary>
    public class FormsAuthenticationService : IAuthenticationService
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
        public FormsAuthenticationService(HttpContextBase httpContext
            , ICustomerService customerService
            , IAdminService adminService
            , IGuestService guestService
            , CustomerSettings customerSettings)
        {
            _httpContext = httpContext;
            _customerService = customerService;
            _adminService = adminService;
            _guestService = guestService;
            _expirationTimeSpan = FormsAuthentication.Timeout;
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

            if (_httpContext?.Request == null
                || !_httpContext.Request.IsAuthenticated
                || !(_httpContext.User.Identity is FormsIdentity))
                return;

            var fromsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var ticketDictionary = StringToAuthenticationTicket(fromsIdentity.Ticket.UserData);

            if (ticketDictionary.ContainsKey(type.ToString()))
                ticketDictionary.Remove(type.ToString());

            //清除已缓存的已身份验证对象
            RemoveCacheMemberByAuthenticationType(type);
        }

        public virtual T GetAuthenticationMember<T>() where T : class
        {
            var authenticationType = GetAuthenticationTypeByMemberType<T>();

            var cacheMember = GetCacheMemberByAuthenticationType(authenticationType);
            if (cacheMember != null)
                return (T)cacheMember;

            if (_httpContext?.Request == null
                || !_httpContext.Request.IsAuthenticated
                || !(_httpContext.User.Identity is FormsIdentity))
                return null;

            var fromsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var ticketDictionary = StringToAuthenticationTicket(fromsIdentity.Ticket.UserData);

            T t = (T)GetMemberByAuthenticationTypeAndTicket(authenticationType, ticketDictionary);

            return t;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 根据身份验证类型枚举获取已通过froms验证的同身份用户对象
        /// </summary>
        private object GetMemberByAuthenticationTypeAndTicket(AuthenticationType type, Dictionary<string, string> ticketDictionary)
        {
            Guid identityGuid;
            if (ticketDictionary.ContainsKey(type.ToString())
                && Guid.TryParse(ticketDictionary[type.ToString()], out identityGuid))
            {
                switch (type)
                {
                    case AuthenticationType.Guest:
                        _cacheGuest = _guestService.GetGuestByGuid(identityGuid);
                        break;
                    case AuthenticationType.Admin:
                        _cacheAdmin = _adminService.GetAdminByGuid(identityGuid);
                        break;
                    case AuthenticationType.Customer:
                        _cacheCustomer = _customerService.GetCustomerByGuid(identityGuid);
                        break;
                    default:
                        throw new ArgumentException(string.Format("Unsupported Authentication Type {0}", type));

                }
                return GetCacheMemberByAuthenticationType(type);
            }
            return null;
        }

        /// <summary>
        /// 通过身份验证类型枚举尝试获得已通过验证的缓存身份对象
        /// </summary>
        private object GetCacheMemberByAuthenticationType(AuthenticationType type)
        {
            switch (type)
            {
                case AuthenticationType.Guest:
                    return _cacheGuest;
                case AuthenticationType.Admin:
                    return _cacheAdmin;
                case AuthenticationType.Customer:
                    return _cacheCustomer;
                default:
                    throw new ArgumentException(string.Format("Unsupported Authentication Type {0}", type));
            }
        }

        /// <summary>
        /// 通过身份验证类型枚举删除已通过验证的缓存身份对象
        /// </summary>
        /// <param name="type"></param>
        private void RemoveCacheMemberByAuthenticationType(AuthenticationType type)
        {
            switch (type)
            {
                case AuthenticationType.Guest:
                    _cacheGuest = null;
                    break;
                case AuthenticationType.Admin:
                    _cacheAdmin = null;
                    break;
                case AuthenticationType.Customer:
                    _cacheCustomer = null;
                    break;
                default:
                    throw new ArgumentException(string.Format("Unsupported Authentication Type {0}", type));
            }
        }

        /// <summary>
        /// 通过类型T获取对应的身份验证类型枚举
        /// </summary>
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

        /// <summary>
        /// 将froms身份验证票据转换为字符串
        /// </summary>
        private string AuthenticationTicketToString(Dictionary<string, string> ticketDictionary)
        {
            var dictionaryTypeConverter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            return (string)dictionaryTypeConverter.ConvertTo(ticketDictionary, typeof(string));
        }

        /// <summary>
        /// 将字符串转换为froms身份验证票据
        /// </summary>
        private Dictionary<string, string> StringToAuthenticationTicket(string ticketString)
        {
            var dictionaryTypeConverter = TypeDescriptor.GetConverter(typeof(Dictionary<string, string>));
            return (Dictionary<string, string>)dictionaryTypeConverter.ConvertFrom(ticketString) ?? new Dictionary<string, string>();
        }

        #endregion

    }

}
