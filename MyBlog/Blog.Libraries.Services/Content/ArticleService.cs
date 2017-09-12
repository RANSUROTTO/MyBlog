using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Services.Content
{
    public class ArticleService : IArticleService
    {

        #region Fields

        private IRepository<Article> _articleRepository;

        #endregion

        #region Constructor

        public ArticleService(IRepository<Article> articleRepository)
        {
            this._articleRepository = articleRepository;
        }

        #endregion

        #region Methods

        public Article GetArticleById(long id)
        {
            throw new NotImplementedException();
        }

        public Article GetArticleByGuid(Guid guid)
        {
            throw new NotImplementedException();
        }

        public void InsertArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void UpdateArticle(Article article)
        {
            throw new NotImplementedException();
        }

        public void DeleteArticle(Article article)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
