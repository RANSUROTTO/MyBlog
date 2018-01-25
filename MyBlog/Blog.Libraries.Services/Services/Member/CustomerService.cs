using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Member
{
	public partial interface ICustomerService : IService<Customer> {}

    public partial class CustomerService : Service<Customer>,ICustomerService 
    {
        
        #region Constructor

        public CustomerService (IRepository<Customer> repository) : base(repository) {}
        
        #endregion

    }

}
