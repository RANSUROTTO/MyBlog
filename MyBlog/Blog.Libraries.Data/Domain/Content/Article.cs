using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Members;

namespace Blog.Libraries.Data.Domain.Content
{

    /// <summary>
    /// 代表一个文章
    /// </summary>
    public class Article : BaseEntity
    {

        /// <summary>
        /// 获取或设置文章标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 获取或设置文章内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置文章作者
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// 获取或设置文章的分类
        /// </summary>
        public virtual Categorie Categorie { get; set; }

    }
}
