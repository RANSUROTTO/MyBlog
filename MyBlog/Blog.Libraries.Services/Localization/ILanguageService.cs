using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Domain.Localization;

namespace Blog.Libraries.Services.Localization
{

    public partial interface ILanguageService
    {

        /// <summary>
        /// 插入语言
        /// </summary>
        /// <param name="language">语言</param>
        void InsertLanguage(Language language);

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="language">语言</param>
        void UpdateLanguage(Language language);

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="language">语言</param>
        void DeleteLanguage(Language language);

        /// <summary>
        /// 通过ID获取语言
        /// </summary>
        /// <param name="languageId">语言ID</param>
        /// <returns>语言</returns>
        Language GetLanguageById(long languageId);
        
    }

}
