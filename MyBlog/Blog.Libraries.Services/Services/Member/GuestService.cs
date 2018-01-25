using Blog.Libraries.Data.Domain.Member;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Member
{
	public partial interface IGuestService : IService<Guest> {}

    public partial class GuestService : Service<Guest>,IGuestService 
    {
        
        #region Constructor

        public GuestService (IRepository<Guest> repository) : base(repository) {}
        
        #endregion

    }

}
