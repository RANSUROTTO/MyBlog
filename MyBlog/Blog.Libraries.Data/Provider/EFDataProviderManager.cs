using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Provider
{

    /// <summary>
    /// Entity Framework 数据提供者管理类
    /// </summary>
    // ReSharper disable once InconsistentNaming
    public class EFDataProviderManager : BaseDataProviderManager
    {

        /// <summary>
        /// Ctor
        /// </summary>
        public EFDataProviderManager(DataSettings settings) : base(settings)
        {
        }

        /// <summary>
        /// 加载数据提供者
        /// </summary>
        /// <returns>数据提供者</returns>
        protected override IDataProvider LoadDataProvider()
        {
            var providerName = Settings.DataProvider;
            if (string.IsNullOrEmpty(providerName))
                throw new Exception("Settings is null or empty");

            switch (providerName.ToLowerInvariant())
            {
                default:
                    throw new Exception(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }

}
