using Blog.Libraries.Data.Domain.Permissions;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Permissions
{
	public partial interface IUserRoleService : IService<UserRole> {}

    public partial class UserRoleService : Service<UserRole>,IUserRoleService 
    {
        
        #region Constructor

        public UserRoleService (IRepository<UserRole> repository) : base(repository) {}
        
        #endregion

    }

}
