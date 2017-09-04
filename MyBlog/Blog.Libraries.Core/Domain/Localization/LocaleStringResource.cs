using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Core.Domain.Localization
{

    public class LocaleStringResource : BaseEntity
    {

        /// <summary>
        /// 获取或设置资源名称
        /// </summary>
        public string ResourceName { get; set; }

        /// <summary>
        /// 获取或设置资源值
        /// </summary>
        public string ResourceValue { get; set; }

        /// <summary>
        /// 获取或设置语言身份
        /// </summary>
        public Language Language { get; set; }

    }

}
