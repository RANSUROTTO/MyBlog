using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Member
{
	public partial interface IAdminService : IService<Admin> {}

    public partial class AdminService : Service<Admin>,IAdminService 
    {
        
        #region Constructor

        public AdminService (IRepository<Admin> repository) : base(repository) {}
        
        #endregion

    }

}
