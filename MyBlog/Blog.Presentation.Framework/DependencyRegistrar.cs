using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using System.Web;
using Autofac.Integration.Mvc;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Fakes;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using Blog.Libraries.Data.Context;
using Blog.Libraries.Data.Provider;
using Blog.Libraries.Data.Repository;

namespace Blog.Presentation.Framework
{

    /// <summary>
    /// 依赖注册
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {

        /// <summary>
        /// 注册服务
        /// </summary>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, WebConfig config)
        {

            //注册Http上下文和其它相关服务
            builder.Register(p => HttpContext.Current == null
                ? (new FakeHttpContext("~/") as HttpContextBase)
                : (new HttpContextWrapper(HttpContext.Current) as HttpContextBase))
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(p => p.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            //注册 Web Helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //注册 Controller
            builder.RegisterControllers(typeFinder.GetAssemblies().ToArray());

            //注册数据库操作服务
            var dataSettingsManager = new DataSettingsManager();
            var dataSettings = dataSettingsManager.LoadSettings();
            builder.Register(p => dataSettingsManager.LoadSettings())
                .As<DataSettings>();
            builder.Register(p => new EFDataProviderManager(dataSettings))
                .As<BaseDataProviderManager>();
            builder.Register(p => p.Resolve<BaseDataProviderManager>().LoadDataProvider())
                .As<IDataProvider>();

            if (dataSettings != null && dataSettings.IsValid())
            {
                var efDataProviderManager = new EFDataProviderManager(dataSettings);
                var dataProvider = efDataProviderManager.LoadDataProvider();
                dataProvider.InitConnectionFactory();

                builder.Register(p => new EntityContext(dataSettings.DataConnectionString))
                    .As<IDbContext>();
            }
            else
            {
                throw new ArgumentException("dataSettings is invalid");
            }

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            //注册插件服务

            //注册缓存服务




        }

        public int Order
        {
            get { return 0; }
        }

    }

}
