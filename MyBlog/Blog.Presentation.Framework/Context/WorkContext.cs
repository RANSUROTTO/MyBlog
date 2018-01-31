using System.Web;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Core.Domain.Localization;
using Blog.Libraries.Core.Domain.Members;
using Blog.Libraries.Core.Domain.Members.Enum;
using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Services.Authentication;

namespace Blog.Presentation.Framework.Context
{

    public class WorkContext : IWorkContext
    {

        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly IAuthenticationService _authenticationService;

        #endregion

        #region Constructor

        public WorkContext(HttpContextBase httpContext, IAuthenticationService authenticationService)
        {
            _httpContext = httpContext;
            _authenticationService = authenticationService;
        }

        #endregion

        #region Properties

        public virtual IGuest Guest
        {
            get { return _authenticationService.GetAuthenticationMember<Guest>(AuthenticationType.Guest); }
        }

        public ICustomer Customer
        {
            get { return _authenticationService.GetAuthenticationMember<Customer>(AuthenticationType.Customer); }
        }

        public IAdmin Admin
        {
            get { return _authenticationService.GetAuthenticationMember<Admin>(AuthenticationType.Admin); }
        }

        public Language Language { get; set; }

        #endregion

    }

}
