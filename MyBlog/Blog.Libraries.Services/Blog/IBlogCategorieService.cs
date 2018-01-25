using System.Collections.Generic;
using Blog.Libraries.Data.Domain.Blog;

namespace Blog.Libraries.Services.Blog
{

    /// <summary>
    /// 文章分类业务接口
    /// </summary>
    public partial interface IBlogCategorieService
    {

        /// <summary>
        /// 获取所有文章分类
        /// </summary>
        /// <returns>分类集合</returns>
        IList<BlogCategorie> GetAllCategorie();

        /// <summary>
        /// 根据ID获取文章分类
        /// </summary>
        /// <param name="categorieId">文章分类ID</param>
        /// <returns>文章分类</returns>
        BlogCategorie GetCategorieById(long categorieId);

    }

}
