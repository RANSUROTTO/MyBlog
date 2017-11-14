using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using System.Web;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Caching.MemCaching;
using Blog.Libraries.Core.Caching.RedisCaching;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Fakes;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Core.Infrastructure;
using Blog.Libraries.Core.Infrastructure.TypeFinder;
using Blog.Libraries.Data.Context;
using Blog.Libraries.Data.Provider;
using Blog.Libraries.Data.Repository;
using Blog.Libraries.Services.Authentication;
using Blog.Libraries.Services.Configuration;
using Blog.Libraries.Services.Infrastructure.Installation;
using Blog.Libraries.Services.Members;
using Blog.Libraries.Services.Security;

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

            builder.RegisterGeneric(typeof(EfRepository<>))
                .As(typeof(IRepository<>))
                .InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());


            //注册插件服务

            //注册缓存服务
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("cache_per_request").InstancePerLifetimeScope();

            if (config.MemcachedEnable)
                builder.RegisterType<MemcachedManager>().As<ICacheManager>().Named<ICacheManager>("chache_other").SingleInstance();

            if (config.RedisCachingEnable)
            {
                builder.RegisterType<RedisConnectionWrapper>().As<IRedisConnectionWrapper>().SingleInstance();
                builder.RegisterType<RedisCacheManager>().As<ICacheManager>().Named<ICacheManager>("cache_static")
                    .InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("cache_static").SingleInstance();
            }


            //注册设定
            builder.RegisterType<SettingService>().As<ISettingService>()
                .WithParameter(ResolvedParameter.ForNamed<ICacheManager>("cache_static"))
                .InstancePerLifetimeScope();

            //注册业务服务
            builder.RegisterType<InstallationLocalizationService>().As<IInstallationLocalizationService>().InstancePerLifetimeScope();
            builder.RegisterType<CustomerService>().As<ICustomerService>().InstancePerLifetimeScope();
            builder.RegisterType<AdminService>().As<IAdminService>().InstancePerLifetimeScope();
            builder.RegisterType<GuestService>().As<IGuestService>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<FormsAuthenticationService>().As<IAuthenticationService>().InstancePerLifetimeScope();


        }

        public int Order
        {
            get { return 0; }
        }

    }


    public class SettingsSource : IRegistrationSource
    {
        static readonly MethodInfo BuildMethod =
            typeof(SettingsSource).GetMethod("BuildRegistration", BindingFlags.Static | BindingFlags.NonPublic);

        public IEnumerable<IComponentRegistration> RegistrationsFor(
            Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {
            var ts = service as TypedService;
            if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
            {
                var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                yield return (IComponentRegistration)buildMethod.Invoke(null, null);
            }
        }

        static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
        {
            return RegistrationBuilder
                .ForDelegate((c, p) =>
                {
                    return c.Resolve<ISettingService>().LoadSetting<TSettings>();
                })
                .InstancePerLifetimeScope()
                .CreateRegistration();
        }

        public bool IsAdapterForIndividualComponents { get { return false; } }

    }


}
