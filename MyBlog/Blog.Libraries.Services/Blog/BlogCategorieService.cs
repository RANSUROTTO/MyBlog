using System;
using System.Collections.Generic;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Data.Domain.Blog;

namespace Blog.Libraries.Services.Blog
{

    public partial class BlogCategorieService : IBlogCategorieService
    {

        #region Fields

        private readonly IRepository<BlogCategorie> _categorieRepository;

        #endregion
        
        #region Methods

        public IList<BlogCategorie> GetAllCategorie()
        {
            throw new NotImplementedException();
        }

        public BlogCategorie GetCategorieById(long categorieId)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
