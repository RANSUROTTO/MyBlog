﻿using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Libraries.Services.Blog;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.Controllers;

namespace Blog.Presentation.Admin.Controllers
{

    [ControllerDescription(
        "[Name=博文管理,Icon=fa fa-awit,Order=0,I18n=true]"
        , "[Name=文章管理,Icon=fa fa-awit,Order=0,I18n=true]"
        )]
    public class BlogPostController : AdminController
    {

        #region Fields

        private readonly IBlogPostService _blogPostService;

        #endregion

        #region Constructor

        public BlogPostController(IWorkContext workContext, IBlogPostService blogPostService) : base(workContext)
        {
            _blogPostService = blogPostService;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion

    }

}