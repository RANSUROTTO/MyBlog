using Blog.Libraries.Data.Domain.Permissions;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Permissions
{
	public partial interface IRoleGroupService : IService<RoleGroup> {}

    public partial class RoleGroupService : Service<RoleGroup>,IRoleGroupService 
    {
        
        #region Constructor

        public RoleGroupService (IRepository<RoleGroup> repository) : base(repository) {}
        
        #endregion

    }

}
