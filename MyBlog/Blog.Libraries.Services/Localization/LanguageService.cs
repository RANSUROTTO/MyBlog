using System;
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
            if (language == null)
                throw new ArgumentNullException("language");
            _languageRepository.Insert(language);
        }

        public void UpdateLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            _languageRepository.Update(language);
        }

        public void DeleteLanguage(Language language)
        {
            if (language == null)
                throw new ArgumentNullException("language");
            _languageRepository.Delete(language);
        }

        public Language GetLanguageById(long languageId)
        {
            return _languageRepository.GetById(languageId);
        }

        #endregion

    }
}
