using System.Web.Mvc;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using Blog.Presentation.Web.Models.Install;
using Blog.Libraries.Services.Infrastructure.Installation;

namespace Blog.Presentation.Web.Controllers
{

    public class InstallController : Controller
    {

        #region Fields

        private readonly IInstallationLocalizationService _installationLocalzationService;
        private readonly WebConfig _config;

        #endregion

        #region Constructor

        public InstallController(IInstallationLocalizationService locService, WebConfig config)
        {
            _installationLocalzationService = locService;
            _config = config;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult Index()
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            this.Server.ScriptTimeout = 300;

            //default install settings
            var model = new InstallModel
            {
                AdminEmail = "Adminstrator@yourBlog.com",
                InstallSampleData = false,
                NotExistCreateDatabase = false,
                DataProvider = "mysql",
                SqlConnectionInfo = "sqlconnectioninfo_values"
            };

            //builder available language list
            foreach (var lang in _installationLocalzationService.GetAvailableLanguage())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", new { languageCode = lang.Code }),
                    Text = lang.Name,
                    Selected = _installationLocalzationService.GetCurrentLanguage().Code == lang.Code
                });
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InstallModel model)
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            this.Server.ScriptTimeout = 600;

            return View();
        }

        public ActionResult ChangeLanguage(string languageCode)
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            _installationLocalzationService.SaveCurrentLanguage(languageCode);

            return RedirectToAction("Index", "Install");
        }

        public ActionResult RestartInstall()
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            //restart application
            IWebHelper webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            return RedirectToRoute("HomePage");
        }

        #endregion

        #region Utilities



        #endregion

    }

}