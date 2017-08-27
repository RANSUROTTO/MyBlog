using Blog.Libraries.Core.Common;
using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Provider
{

    /// <summary>
    /// Entity Framework 数据提供者管理类
    /// </summary>
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
                throw new SiteException("数据设置中没有包含的数据提供者名称");

            //Create Provider
            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                case "mysql":
                    return new MySqlDataProvider();
                case "sqlce":
                    return new SqlCeDataProvider();
                default:
                    throw new SiteException(string.Format("不支持的数据提供者名称：{0}", providerName));
            }
        }

    }

}
