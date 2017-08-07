using System;

namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 数据源提供者管理
    /// </summary>
    public abstract class DataProviderManager
    {

        protected DataProviderManager(DataSettings settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");
            this.Settings = settings;
        }

        /// <summary>
        /// 数据源设置
        /// </summary>
        protected DataSettings Settings { get; set; }

        /// <summary>
        /// 加载数据源提供者
        /// </summary>
        /// <returns>数据源提供者</returns>
        public abstract IDataProvider LoadDataProvider();

    }

}
