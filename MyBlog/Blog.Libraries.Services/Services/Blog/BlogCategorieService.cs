using Blog.Libraries.Data.Domain.Blog;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Blog
{
	public partial interface IBlogCategorieService : IService<BlogCategorie> {}

    public partial class BlogCategorieService : Service<BlogCategorie>,IBlogCategorieService 
    {
        
        #region Constructor

        public BlogCategorieService (IRepository<BlogCategorie> repository) : base(repository) {}
        
        #endregion

    }

}
