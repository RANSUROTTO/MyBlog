using System;

namespace Blog.Libraries.Services.Blog
{
    public partial interface IBlogPostService
    {

        /// <summary>
        /// 通过ID获取文章
        /// </summary>
        /// <param name="articleId">文章ID</param>
        /// <returns>一篇文章</returns>
        Data.Domain.Blog.BlogPost GetArticleById(long articleId);

        /// <summary>
        /// 通过Guid获取文章
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>文章</returns>
        Data.Domain.Blog.BlogPost GetArticleByGuid(Guid guid);

        /// <summary>
        /// 插入文章
        /// </summary>
        /// <param name="blogPost">文章</param>
        void InsertArticle(Data.Domain.Blog.BlogPost blogPost);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="blogPost">文章</param>
        void UpdateArticle(Data.Domain.Blog.BlogPost blogPost);

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="blogPost">文章</param>
        void DeleteArticle(Data.Domain.Blog.BlogPost blogPost);

    }
}
