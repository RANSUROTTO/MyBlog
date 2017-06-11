using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Infrastructure.DependencyManagement;
using Blog.Libraries.Core.Infrastructure.TypeFinder;

namespace Blog.Libraries.Core.Infrastructure
{
    /// <summary>
    /// 项目引擎
    /// </summary>
    public class SiteEngine : IEngine
    {
        #region Fields

        private ContainerManager _containerManager;

        #endregion

        #region Properties

        /// <summary>
        /// 集装箱管理
        /// </summary>
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }

        #endregion

        #region Methods



        #endregion

        #region Utilities

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <param name="config"></param>
        public virtual void RegisterDependencies(WebConfig config)
        {
            var builder = new ContainerBuilder();
            var container = builder.Build();
            this._containerManager = new ContainerManager(container);



        }

        /// <summary>
        /// 运行应用启动任务
        /// </summary>
        public virtual void RunStartupTasks()
        {
            //finder subclass
            var typeFinder = _containerManager.Resolve<ITypeFinder>();
            var startUpTaskTypes = typeFinder.FindClassesOfType<IStartupTask>();
            //builder examples
            var startUpTasks = startUpTaskTypes.Select(startUpTaskType => (IStartupTask)Activator.CreateInstance(startUpTaskType)).ToList();
            //sort
            startUpTasks = startUpTasks.AsQueryable().OrderBy(st => st.Order).ToList();
            //Execute
            startUpTasks.ForEach(p => p.Execute());
        }

        #endregion

    }
}
