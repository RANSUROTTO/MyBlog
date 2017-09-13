using System.Collections.Generic;
using System.Linq;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Domain.Blog
{

    /// <summary>
    /// 代表一个分类
    /// </summary>
    public class BlogCategorie : BaseEntity
    {

        /// <summary>
        /// 获取或设置分类的名称
        /// </summary>
        public string Title { get; set; }


        private ICollection<BlogPost> _articles;

        /// <summary>
        /// 获取或设置该分类下的文章列表
        /// </summary>
        public virtual ICollection<BlogPost> Articles
        {
            get { return _articles?.Where(p => !p.IsDeleted)?.ToList() ?? new List<BlogPost>(); }
            set { _articles = value; }
        }

    }

}
