using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Member
{
	public partial interface ICustomerPasswordService : IService<CustomerPassword> {}

    public partial class CustomerPasswordService : Service<CustomerPassword>,ICustomerPasswordService 
    {
        
        #region Constructor

        public CustomerPasswordService (IRepository<CustomerPassword> repository) : base(repository) {}
        
        #endregion

    }

}
