using System.Collections.Generic;
using System.Linq;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Content
{

    /// <summary>
    /// 代表一个分类
    /// </summary>
    public class Categorie : BaseEntity
    {

        /// <summary>
        /// 获取或设置分类的名称
        /// </summary>
        public string Title { get; set; }


        private ICollection<Article> _articles;

        /// <summary>
        /// 获取或设置该分类下的文章列表
        /// </summary>
        public virtual ICollection<Article> Articles
        {
            get { return _articles?.Where(p => !p.IsDeleted)?.ToList() ?? new List<Article>(); }
            set { _articles = value; }
        }

    }

}
