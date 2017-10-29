using System;
using System.Threading;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Data.Provider;
using Blog.Presentation.Web.Models.Install;
using Blog.Libraries.Services.Infrastructure.Installation;

namespace Blog.Presentation.Web.Controllers
{

    public class InstallController : Controller
    {

        #region Fields

        private readonly IInstallationLocalizationService _installationLocalizationService;
        private readonly WebConfig _config;

        #endregion

        #region Constructor

        public InstallController(IInstallationLocalizationService locService, WebConfig config)
        {
            _installationLocalizationService = locService;
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

            //初始化安装默认参数
            var model = new InstallModel
            {
                AdminEmail = "Adminstrator@yourBlog.com",
                InstallSampleData = false,
                NotExistCreateDatabase = false,
                DataProvider = "mysql",
                SqlConnectionInfo = "sqlconnectioninfo_values"
            };

            //构建页面可用语言列表
            foreach (var lang in _installationLocalizationService.GetAvailableLanguage())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", new { languageCode = lang.Code }),
                    Text = lang.Name,
                    Selected = _installationLocalizationService.GetCurrentLanguage().Code == lang.Code
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

            //设置页面超时时间为10分钟
            this.Server.ScriptTimeout = 600;

            if (model.DatabaseConnectionString != null)
                model.DatabaseConnectionString = model.DatabaseConnectionString.Trim();

            //构建页面可用语言列表
            foreach (var lang in _installationLocalizationService.GetAvailableLanguage())
            {
                model.AvailableLanguages.Add(new SelectListItem
                {
                    Value = Url.Action("ChangeLanguage", new { languageCode = lang.Code }),
                    Text = lang.Name,
                    Selected = _installationLocalizationService.GetCurrentLanguage().Code == lang.Code
                });
            }

            //使用mysql数据库，检查连接字符串
            if (model.DataProvider.Equals("mysql", StringComparison.InvariantCultureIgnoreCase))
            {
                if (model.SqlConnectionInfo.Equals("sqlDatabaseConnectionString", StringComparison.InvariantCultureIgnoreCase))
                {
                    if (string.IsNullOrEmpty(model.DatabaseConnectionString))
                        ModelState.AddModelError("", _installationLocalizationService.GetResource("DatabaseConnectionStringRequired"));

                    try
                    {
                        new MySqlConnectionStringBuilder(model.DatabaseConnectionString);
                    }
                    catch
                    {
                        ModelState.AddModelError("",
                            _installationLocalizationService.GetResource("DatabaseConnectionStringError"));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(model.ServerName))
                        ModelState.AddModelError("", _installationLocalizationService.GetResource("ServerNameRequired"));

                    if (string.IsNullOrEmpty(model.DatabaseName))
                        ModelState.AddModelError("", _installationLocalizationService.GetResource("DatabaseNameRequired"));

                    if (string.IsNullOrEmpty(model.ServerUsername))
                        ModelState.AddModelError("", _installationLocalizationService.GetResource("ServerUsernameRequired"));

                    if (string.IsNullOrEmpty(model.ServerUserPassword))
                        ModelState.AddModelError("", _installationLocalizationService.GetResource("ServerUserPasswordRequired"));
                }
            }

            IWebHelper webHelper = EngineContext.Current.Resolve<IWebHelper>();

            if (ModelState.IsValid)
            {
                var settingsManager = new DataSettingsManager();
                try
                {
                    string connectionString;
                    if (model.DataProvider.Equals("mysql", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //mysql
                        if (model.SqlConnectionInfo.Equals("sqlDatabaseInfo",
                            StringComparison.InvariantCultureIgnoreCase))
                        {
                            connectionString = CreateConnectionString(model.ServerName, model.DatabaseName,
                                model.ServerUsername, model.ServerUserPassword);
                        }
                        else
                        {
                            var sqlConnectionStringBuilder =
                                new MySqlConnectionStringBuilder(model.DatabaseConnectionString);
                            connectionString = sqlConnectionStringBuilder.ToString();
                        }

                        if (model.NotExistCreateDatabase)
                        {
                            if (!DatabaseExists(connectionString))
                            {

                            }
                        }
                        else
                        {
                            if (!DatabaseExists(connectionString))
                            {
                                throw new Exception(_installationLocalizationService.GetResource("DatabaseNotExists"));
                            }
                        }
                    }
                    else
                    {
                        throw new Exception(_installationLocalizationService.GetResource("ProviderNotMysql"));
                    }

                    //save settings
                    var dataProvider = model.DataProvider;
                    var settings = new DataSettings
                    {
                        DataProvider = dataProvider,
                        DataConnectionString = connectionString
                    };
                    settingsManager.SaveSettings(settings);

                    //初始化数据库
                    var dataProviderInstance = EngineContext.Current.Resolve<BaseDataProviderManager>().LoadDataProvider();
                    dataProviderInstance.InitDatabase();

                    //插入初始化数据

                    //刷新缓存
                    DataSettingsHelper.ResetCache();

                    //重启应用程序
                    webHelper.RestartAppDomain();

                    //重定向至首页
                    return RedirectToRoute("HomePage");

                }
                catch (Exception exception)
                {
                    //刷新缓存
                    DataSettingsHelper.ResetCache();

                    //清空异常的数据库连接信息
                    settingsManager.SaveSettings(new DataSettings
                    {
                        DataProvider = null,
                        DataConnectionString = null
                    });

                    ModelState.AddModelError("", string.Format(_installationLocalizationService.GetResource("SetupFailed"), exception.Message));
                }
            }
            return View(model);
        }

        public ActionResult ChangeLanguage(string languageCode)
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            _installationLocalizationService.SaveCurrentLanguage(languageCode);

            return RedirectToAction("Index", "Install");
        }

        public ActionResult RestartInstall()
        {
            if (DataSettingsHelper.DatabaseInstalled())
                return RedirectToRoute("HomePage");

            //重启应用程序
            IWebHelper webHelper = EngineContext.Current.Resolve<IWebHelper>();
            webHelper.RestartAppDomain();

            return RedirectToRoute("HomePage");
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 通过连接基础属性创建数据库连接字符串(仅mysql
        /// </summary>
        [NonAction]
        protected virtual string CreateConnectionString(
            string serverName, string databaseName,
            string userName, string password)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                IntegratedSecurity = true,
                Server = serverName,
                Database = databaseName,
                PersistSecurityInfo = false,
                UserID = userName,
                Password = password
            };

            return builder.ConnectionString;
        }

        /// <summary>
        /// 检查指定的数据库是否存在，如果数据库存在，则返回true(仅mysql
        /// </summary>
        [NonAction]
        protected virtual bool DatabaseExists(string connectionString)
        {
            try
            {
                //尝试连接
                using (var conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 创建数据库
        /// </summary>
        [NonAction]
        protected virtual string CreateDatabase(string connectionString, int triesToConnect = 0)
        {
            try
            {
                //获得数据库名称
                var builder = new MySqlConnectionStringBuilder(connectionString);
                var databaseName = builder.Database;

                //创建指向mysql主数据库的连接
                builder.Server = "mysql";
                var masterCatalogConnectionString = builder.ToString();
                string query = string.Format("CREATE DATABASE `{0}`", databaseName);

                //连接数据库执行创建数据库脚本
                using (var conn = new MySqlConnection(masterCatalogConnectionString))
                {
                    conn.Open();
                    using (var command = new MySqlCommand(query, conn))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                //尝试连接
                if (triesToConnect > 0)
                {
                    for (var i = 0; i <= triesToConnect; i++)
                    {
                        if (i == triesToConnect)
                            throw new Exception("Unable to connect to the new database. Please try one more time");

                        if (!this.DatabaseExists(connectionString))
                            Thread.Sleep(1000);
                        else
                            break;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Format(_installationLocalizationService.GetResource("DatabaseCreationError"), ex.Message);
            }
        }

        #endregion

    }

}