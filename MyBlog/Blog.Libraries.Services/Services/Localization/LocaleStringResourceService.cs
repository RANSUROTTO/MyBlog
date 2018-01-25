using Blog.Libraries.Core.Domain.Localization;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Localization
{
	public partial interface ILocaleStringResourceService : IService<LocaleStringResource> {}

    public partial class LocaleStringResourceService : Service<LocaleStringResource>,ILocaleStringResourceService 
    {
        
        #region Constructor

        public LocaleStringResourceService (IRepository<LocaleStringResource> repository) : base(repository) {}
        
        #endregion

    }

}
