using System.Collections.Generic;

namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 数据源设置 (连接字符串信息)
    /// </summary>
    public class DataSettings
    {

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        public DataSettings()
        {
            RawDataSettings = new Dictionary<string, string>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// 数据源提供商  ../mysql,sqlserver,oracle,sqllite
        /// </summary>
        public string DataProvider { get; set; }

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string DataConnectionString { get; set; }

        /// <summary>
        /// 格式化数据源信息
        /// </summary>
        public IDictionary<string, string> RawDataSettings { get; private set; }

        #endregion

        #region Utilities

        /// <summary>
        /// 获取一个值,指示数据源连接信息是否有效
        /// </summary>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(this.DataProvider)
                && !string.IsNullOrEmpty(this.DataConnectionString);
        }

        #endregion

    }

}
