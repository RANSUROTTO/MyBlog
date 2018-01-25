using Blog.Libraries.Data.Domain.Blog;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Services.Services;

namespace Blog.Libraries.Services.Blog
{
	public partial interface IBlogPostService : IService<BlogPost> {}

    public partial class BlogPostService : Service<BlogPost>,IBlogPostService 
    {
        
        #region Constructor

        public BlogPostService (IRepository<BlogPost> repository) : base(repository) {}
        
        #endregion

    }

}
