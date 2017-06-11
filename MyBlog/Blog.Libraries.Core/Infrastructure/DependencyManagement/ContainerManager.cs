using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Core.Lifetime;
using Autofac.Integration.Mvc;

namespace Blog.Libraries.Core.Infrastructure.DependencyManagement
{

    /// <summary>
    /// 容器管理
    /// </summary>
    public class ContainerManager
    {

        #region Fields

        private readonly IContainer _container;

        #endregion

        #region Ctor

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="container">容器</param>
        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获得容器
        /// </summary>
        public virtual IContainer Container
        {
            get
            {
                return _container;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 通过指定类型进行解析
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务</returns>
        public virtual object Resolve(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        /// <summary>
        /// 通过key值和指定类型进行解析
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务</returns>
        public virtual T Resolve<T>(string key = "", ILifetimeScope scope = null) where T : class
        {
            if (scope == null)
            {
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                //不指定键值则获取仅通过指定T类型进行解析
                return scope.Resolve<T>();
            }
            return scope.ResolveKeyed<T>(key);
        }


        /// <summary>
        /// 通过key值和类型T解析指定类型T的所有注册项
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">键值</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务列表</returns>
        public virtual T[] ResolveAll<T>(string key = "", ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }
            return scope.ResolveKeyed<IEnumerable<T>>(key).ToArray();
        }

        /// <summary>
        /// 解析未注册的服务
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务</returns>
        public virtual T ResolveUnregistered<T>(ILifetimeScope scope = null) where T : class
        {
            return ResolveUnregistered(typeof(T), scope) as T;
        }

        /// <summary>
        /// 解析未注册的服务
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务</returns>
        public virtual object ResolveUnregistered(Type type, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            var constructors = type.GetConstructors();
            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters();
                    var parameterInstances = new List<object>();
                    foreach (var parameter in parameters)
                    {
                        var service = Resolve(parameter.ParameterType, scope);
                        if (service == null) throw new Exception("Unknown dependency");
                        parameterInstances.Add(service);
                    }
                    return Activator.CreateInstance(type, parameterInstances.ToArray());
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            throw new Exception("No constructor  was found that had all the dependencies satisfied.");
        }

        /// <summary>
        /// 尝试解析指定解析范围内的服务
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <param name="instance">解析服务</param>
        /// <returns>指示服务是否已成功解决的值</returns>
        public virtual bool TryResolve(Type serviceType, ILifetimeScope scope, out object instance)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.TryResolve(serviceType, out instance);
        }

        /// <summary>
        /// 检查一些服务在指定解析范围内是否已注册（可以解析）
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <param name="scope">解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>结果</returns>
        public virtual bool IsRegistered(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                scope = Scope();
            }
            return scope.IsRegistered(serviceType);
        }

        /// <summary>
        /// 尝试解析指定范围内的服务,解析失败返回null
        /// </summary>
        /// <param name="serviceType">类型</param>
        /// <param name="scope">>解析范围; 传递null以自动解析当前作用域</param>
        /// <returns>解析后的服务</returns>
        public virtual object ResolveOptional(Type serviceType, ILifetimeScope scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.ResolveOptional(serviceType);
        }

        /// <summary>
        /// 获取当前解析范围
        /// </summary>
        /// <returns>解析范围</returns>
        public virtual ILifetimeScope Scope()
        {
            try
            {
                if (HttpContext.Current != null)
                    return AutofacDependencyResolver.Current.RequestLifetimeScope;

                //当返回这样的生命范围时，你应该确定它将被处理一次使用（例如在计划任务中）
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
            catch (Exception)
            {
                //如果RequestLifetimeScope已经处理，我们可以在此处获取异常
                //例如，在“Application_EndRequest”处理程序中或之后请求
                //但是请注意，通常它永远不会发生

                //当返回这样的生命范围时，你应该确定它将被处理一次使用（例如在计划任务中）
                return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag);
            }
        }

        #endregion

    }
}
