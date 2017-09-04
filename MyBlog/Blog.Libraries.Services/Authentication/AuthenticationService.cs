using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Blog.Libraries.Data.Domain.Members;
using Blog.Libraries.Services.Members;

namespace Blog.Libraries.Services.Authentication
{

    /// <summary>
    /// 身份验证业务实现
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly ICustomerService _customerService;
        private readonly TimeSpan _expirationTimeSpan;

        private Guest _cacheGuest;
        private Customer _cacheCustomer;
        private Admin _cacheAdmin;

        #endregion

        #region Constructor

        public AuthenticationService(
            HttpContextBase httpContext
            , ICustomerService customerService
            , TimeSpan expirationTimeSpan)
        {
            _httpContext = httpContext;
            _customerService = customerService;
            _expirationTimeSpan = expirationTimeSpan;
        }

        #endregion

        public virtual void SignIn(Guid guid, AuthenticationType type, DateTime? expirantion = null)
        {
            throw new NotImplementedException();
        }

        public virtual void SignOut(Guid guid, AuthenticationType type)
        {
            throw new NotImplementedException();
        }

        public virtual T GetAuthenticationMember<T>(AuthenticationType type)
        {
            throw new NotImplementedException();
        }

        #region Utilities



        #endregion


    }

}
