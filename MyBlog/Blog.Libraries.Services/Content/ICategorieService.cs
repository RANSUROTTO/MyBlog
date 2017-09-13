using System;
using System.Collections.Generic;
using Blog.Libraries.Data.Domain.Content;

namespace Blog.Libraries.Services.Content
{

    /// <summary>
    /// 文章分类业务接口
    /// </summary>
    public interface ICategorieService
    {

        /// <summary>
        /// 获取所有文章分类
        /// </summary>
        /// <returns>分类集合</returns>
        IList<Categorie> GetAllCategorie();

        /// <summary>
        /// 根据ID获取文章分类
        /// </summary>
        /// <param name="categorieId">文章分类ID</param>
        /// <returns>文章分类</returns>
        Categorie GetCategorieById(long categorieId);

    }

}
