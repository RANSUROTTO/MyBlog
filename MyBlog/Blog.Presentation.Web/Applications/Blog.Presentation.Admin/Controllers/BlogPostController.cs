using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Presentation.Framework.Attributes;
using Blog.Presentation.Framework.Controllers;

namespace Blog.Presentation.Admin.Controllers
{

    [ControllerDescription("[Name=test,Icon=fa fa-awit,Order=0,I18n=true]", "[Name=abc,Icon=fa fa-awit,Order=0,I18n=true]")]
    public class BlogPostController : AdminController 
    {

        #region Constructor

        public BlogPostController(IWorkContext workContext) : base(workContext)
        {
        }

        #endregion

    }

}