using System;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Services.Blog
{
    public class BlogPostService : IBlogPostService
    {

        #region Fields

        private IRepository<Data.Domain.Blog.BlogPost> _articleRepository;

        #endregion

        #region Constructor

        public BlogPostService(IRepository<Data.Domain.Blog.BlogPost> articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        #endregion

        #region Methods

        public Data.Domain.Blog.BlogPost GetArticleById(long articleId)
        {
            throw new NotImplementedException();
        }

        public Data.Domain.Blog.BlogPost GetArticleByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void InsertArticle(Data.Domain.Blog.BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Data.Domain.Blog.BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(Data.Domain.Blog.BlogPost blogPost)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
