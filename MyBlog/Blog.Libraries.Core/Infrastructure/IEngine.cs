using System;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Infrastructure.DependencyManagement;

namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 实现该接口可作为构成提供项目各种服务的引擎
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 容器管理对象
        /// </summary>
        ContainerManager ContainerManager { get; }

        /// <summary>
        /// 初始化运行环境中的组件和插件
        /// </summary>
        /// <param name="config">配置</param>
        void Initialize(WebConfig config);

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        T Resolve<T>() where T : class;

        /// <summary>
        ///  解析依赖
        /// </summary>
        /// <param name="type">类型</param>
        object Resolve(Type type);

        /// <summary>
        /// 解析依赖
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        T[] ResolveAll<T>();

    }
}
