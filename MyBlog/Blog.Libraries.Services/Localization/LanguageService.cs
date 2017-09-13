using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Domain.Localization;

namespace Blog.Libraries.Services.Localization
{
    public class LanguageService : ILanguageService
    {

        #region Fields

        private readonly IRepository<Language> _languageRepository;

        #endregion

        #region Constructor

        public LanguageService(IRepository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        #endregion

        #region Methods

        public void InsertLanguage(Language language)
        {
            throw new NotImplementedException();
        }

        public void UpdateLanguage(Language language)
        {
            throw new NotImplementedException();
        }

        public void DeleteLanguage(Language language)
        {
            throw new NotImplementedException();
        }

        public Language GetLanguageById(long languageId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
