using System.Web;
using Blog.Libraries.Core.Domain.Members;

namespace Blog.Presentation.Framework.Attributes.AuthorizeAttributes
{

    public class AdminAuthorizeAttribute : RoleAuthorizeAttribute
    {

        #region Constructor

        public AdminAuthorizeAttribute(string roleCode) : base(roleCode)
        {
        }

        public AdminAuthorizeAttribute(RoleActionType roleActionType) : base(roleActionType)
        {
        }

        public AdminAuthorizeAttribute(string roleCode, AuthenticationType type) : base(roleCode, type)
        {
        }

        public AdminAuthorizeAttribute(RoleActionType roleActionType, AuthenticationType type) : base(roleActionType, type)
        {
        }

        #endregion

        #region Methods

        public override void AuthorizeFail(HttpResponseBase response)
        {
            response.RedirectToRoute("Admin_Login");
            response.End();
        }

        #endregion

    }

}
