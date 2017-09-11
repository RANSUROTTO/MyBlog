using System;
using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Services.Content
{
    public interface IArticleService
    {

        /// <summary>
        /// 通过ID获取文章
        /// </summary>
        /// <param name="id">文章ID</param>
        /// <returns>一篇文章</returns>
        Article GetArticleById(long id);

        /// <summary>
        /// 通过Guid获取文章
        /// </summary>
        /// <param name="guid">Guid</param>
        /// <returns>文章</returns>
        Article GetArticleByGuid(Guid guid);

        /// <summary>
        /// 插入文章
        /// </summary>
        /// <param name="article">文章</param>
        void InsertArticle(Article article);

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="article">文章</param>
        void UpdateArticle(Article article);

        /// <summary>
        /// 通过ID删除一篇文章
        /// </summary>
        /// <param name="id">文章ID</param>
        void DeleteArticle(long id);

    }
}
