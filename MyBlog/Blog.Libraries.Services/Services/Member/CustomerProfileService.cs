using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Member
{
	public partial interface ICustomerProfileService : IService<CustomerProfile> {}

    public partial class CustomerProfileService : Service<CustomerProfile>,ICustomerProfileService 
    {
        
        #region Constructor

        public CustomerProfileService (IRepository<CustomerProfile> repository) : base(repository) {}
        
        #endregion

    }

}
