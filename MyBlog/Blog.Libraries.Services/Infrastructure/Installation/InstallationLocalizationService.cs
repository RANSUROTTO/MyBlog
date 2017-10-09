using System;
using System.Collections.Generic;

namespace Blog.Libraries.Services.Infrastructure.Installation
{

    public class InstallationLocalizationService : IInstallationLocalizationService
    {

        public string GetResource(string resourceName)
        {
            throw new NotImplementedException();
        }

        public InstallationLanguage GetCurrentLanguage()
        {
            throw new NotImplementedException();
        }

        public void SaveCurrentLanguage(string languageCode)
        {
            throw new NotImplementedException();
        }

        public IList<InstallationLanguage> GetAvailableLanguage()
        {
            throw new NotImplementedException();
        }

    }

}
