using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Domain.Blog
{

    /// <summary>
    /// 代表一个博文
    /// </summary>
    public class BlogPost : BaseEntity
    {

        /// <summary>
        /// 获取或设置博文标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置博文内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 获取或设置博文的概述
        /// 如果不为空,展示时则不显示博文内容,而是显示概述
        /// </summary>
        public string BodyOverview { get; set; }

        /// <summary>
        /// 获取或设置一个值,指示是否允许评论
        /// </summary>
        public bool AllowComments { get; set; }






        /// <summary>
        /// 获取或设置文章作者
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// 获取或设置文章的分类
        /// </summary>
        public virtual BlogCategorie BlogCategorie { get; set; }

    }
}
