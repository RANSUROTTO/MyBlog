using Blog.Libraries.Core.Data;

namespace Blog.Libraries.Data.Provider
{

    /// <summary>
    /// 数据提供者管理基类
    /// </summary>
    public abstract class BaseDataProviderManager
    {

        /// <summary>
        /// 数据源设置信息
        /// </summary>
        protected DataSettings Settings { get; set; }

        /// <summary>
        /// Ctor
        /// </summary>
        protected BaseDataProviderManager(DataSettings settings)
        {
            this.Settings = settings;
        }

        /// <summary>
        /// 加载数据提供者
        /// </summary>
        /// <returns>数据提供者</returns>
        public abstract IDataProvider LoadDataProvider();

    }

}
