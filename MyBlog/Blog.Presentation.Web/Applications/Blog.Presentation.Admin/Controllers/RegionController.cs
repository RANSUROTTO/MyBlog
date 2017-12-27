using System.Linq;
using System.Web.Mvc;
using Blog.Libraries.Core.Context;
using Blog.Presentation.Framework.Controllers;
using Blog.Presentation.Framework.Services.Controller;

namespace Blog.Presentation.Admin.Controllers
{
    public class RegionController : AdminController
    {

        #region fields

        private readonly IRegionService _regionService;

        #endregion

        #region Constructor

        public RegionController(IWorkContext workContext, IRegionService regionService) : base(workContext)
        {
            _regionService = regionService;
        }

        #endregion

        public ActionResult Sidebar()
        {
            var adminMenus = _regionService.GetAdminMenus(0).ToList();
            return View(adminMenus);
        }

        public ActionResult Header()
        {
            return View();
        }

        public ActionResult Footer()
        {
            return View();
        }

    }
}