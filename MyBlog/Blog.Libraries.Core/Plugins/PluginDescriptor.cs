using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Blog.Libraries.Core.Infrastructure;

namespace Blog.Libraries.Core.Plugins
{

    /// <summary>
    /// 插件模型
    /// </summary>
    public class PluginDescriptor : IComparable<PluginDescriptor>
    {

        #region Ctor

        public PluginDescriptor()
        {
            this.SupportedVersions = new List<string>();
            this.LimitedToStores = new List<int>();
            this.LimitedToCustomerRoles = new List<int>();
        }

        public PluginDescriptor(Assembly referencedAssembly, FileInfo originalAssemblyFile,
            Type pluginType)
            : this()
        {
            this.ReferencedAssembly = referencedAssembly;
            this.OriginalAssemblyFile = originalAssemblyFile;
            this.PluginType = pluginType;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Plugin type
        /// </summary>
        public virtual string PluginFileName { get; set; }

        /// <summary>
        /// 插件类型
        /// </summary>
        public virtual Type PluginType { get; set; }

        /// <summary>
        /// The assembly that has been shadow copied that is active in the application
        /// </summary>
        public virtual Assembly ReferencedAssembly { get; internal set; }

        /// <summary>
        /// The original assembly file that a shadow copy was made from it
        /// </summary>
        public virtual FileInfo OriginalAssemblyFile { get; internal set; }

        /// <summary>
        /// 插件组
        /// </summary>
        public virtual string Group { get; set; }

        /// <summary>
        /// 友好名称
        /// </summary>
        public virtual string FriendlyName { get; set; }

        /// <summary>
        /// 系统名称
        /// </summary>
        public virtual string SystemName { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public virtual string Version { get; set; }

        /// <summary>
        /// 支持的版本
        /// </summary>
        public virtual IList<string> SupportedVersions { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public virtual string Author { get; set; }

        /// <summary>
        /// 描述信息
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public virtual int DisplayOrder { get; set; }

        /// <summary>
        /// 是否已安装
        /// </summary>
        public virtual bool Installed { get; set; }



        /// <summary>
        /// Gets or sets the list of store identifiers in which this plugin is available. If empty, then this plugin is available in all stores
        /// </summary>
        public virtual IList<int> LimitedToStores { get; set; }

        /// <summary>
        /// Gets or sets the list of customer role identifiers for which this plugin is available. If empty, then this plugin is available for all ones.
        /// </summary>
        public virtual IList<int> LimitedToCustomerRoles { get; set; }


        #endregion

        #region Methods

        public virtual T Instance<T>() where T : class, IPlugin
        {
            object instance;
            if (!EngineContext.Current.ContainerManager.TryResolve(PluginType, null, out instance))
            {
                //not resolve
                instance = EngineContext.Current.ContainerManager.ResolveUnregistered(PluginType);
            }
            var typedInstance = instance as T;
            if (typedInstance != null)
                typedInstance.PluginDescriptor = this;
            return typedInstance;
        }

        public virtual IPlugin Instance()
        {
            return Instance<IPlugin>();
        }

        #endregion

        #region Utilities

        /// <summary>
        /// 实现该方法可以帮助PluginDescriptor集合进行排序
        /// </summary>
        public int CompareTo(PluginDescriptor other)
        {
            if (DisplayOrder != other.DisplayOrder)
                return DisplayOrder.CompareTo(other.DisplayOrder);

            return string.Compare(FriendlyName, other.FriendlyName, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return FriendlyName;
        }

        public override int GetHashCode()
        {
            return SystemName.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var other = obj as PluginDescriptor;
            return other != null && SystemName != null && SystemName.Equals(other.SystemName);
        }

        #endregion

    }
}
