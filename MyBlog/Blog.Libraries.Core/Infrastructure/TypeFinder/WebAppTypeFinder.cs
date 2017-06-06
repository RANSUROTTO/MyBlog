using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Blog.Libraries.Core.Infrastructure.TypeFinder
{

    public class WebAppTypeFinder : AppDomainTypeFinder
    {

        #region Fields

        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置是否应特别检查Web应用程序的bin文件夹中的程序集是否在应用程序加载时加载。 在应用程序重新加载后，需要在AppDomain中加载插件的情况下才需要这样做。
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取 \Bin 目录的物理磁盘路径
        /// </summary>
        /// <returns>物理路径</returns>
        public virtual string GetBinDirectory()
        {
            if (HostingEnvironment.IsHosted)
            {
                return HttpRuntime.BinDirectory;
            }
            //没有托管 例如，在单元测试中运行
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                string binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }
            return base.GetAssemblies();
        }

        #endregion

    }
}
