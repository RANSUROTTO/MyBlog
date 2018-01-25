using Blog.Libraries.Data.Domain.Configuration;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Configuration
{
	public partial interface ISettingService : IService<Setting> {}

    public partial class SettingService : Service<Setting>,ISettingService 
    {
        
        #region Constructor

        public SettingService (IRepository<Setting> repository) : base(repository) {}
        
        #endregion

    }

}
