using System.Collections.Generic;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Core.Domain.Localization
{

    /// <summary>
    /// 代表一种语言
    /// </summary>
    public class Language : BaseEntity
    {

        /// <summary>
        /// 获取或设置名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置语言文化
        /// </summary>
        public string LanguageCulture { get; set; }

        /// <summary>
        /// 获取或设置唯一的SEO代码
        /// </summary>
        public string UniqueSeoCode { get; set; }

        /// <summary>
        /// 获取或设置显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }


        private ICollection<LocaleStringResource> _localeStringResources;

        /// <summary>
        /// 获取或设置对应的语言字符串资源
        /// </summary>
        public virtual ICollection<LocaleStringResource> LocaleStringResources
        {
            get
            {
                return _localeStringResources ?? (_localeStringResources = new List<LocaleStringResource>());
            }
            set
            {
                _localeStringResources = value;
            }
        }

    }

}
