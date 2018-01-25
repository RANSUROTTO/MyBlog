using Blog.Libraries.Core.Domain.Localization;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Localization
{
	public partial interface ILanguageService : IService<Language> {}

    public partial class LanguageService : Service<Language>,ILanguageService 
    {
        
        #region Constructor

        public LanguageService (IRepository<Language> repository) : base(repository) {}
        
        #endregion

    }

}
