using System.Web.Mvc;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Configuration;
using Blog.Presentation.Web.Models.Install;
using Blog.Libraries.Services.Infrastructure.Installation;

namespace Blog.Presentation.Web.Controllers
{

    public class InstallController : Controller
    {

        #region Fields

        private readonly IInstallationLocalizationService _locService;
        private readonly WebConfig _config;

        #endregion

        #region Constructor

        public InstallController(IInstallationLocalizationService locService, WebConfig config)
        {
            _locService = locService;
            _config = config;
        }

        #endregion

        #region Methods

        [HttpGet]
        public ActionResult Index()
        {
            //数据库已安装
            if (!DataSettingsHelper.DatabaseInstalled())
                return RedirectToAction("Index", "Home");

            //设置请求超时时间 (设置为5分钟)
            this.Server.ScriptTimeout = 300;

            //创建默认安装设置模型
            var model = new InstallModel
            {
                AdminEmail = "Adminstrator@yourBlog.com",
                InstallSampleData = false,
                NotExistCreateDatabase = false,
                DataProvider = "mysql",
                SqlConnectionInfo = "sqlconnectioninfo_values"
            };

            //获取可用的语言


            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Action(InstallModel model)
        {
            //数据库已安装
            if (!DataSettingsHelper.DatabaseInstalled())
                return RedirectToAction("Index", "Home");

            //设置请求超时时间 (设置为10分钟)
            this.Server.ScriptTimeout = 600;




            return View();
        }

        #endregion

        #region Utilities



        #endregion

    }

}