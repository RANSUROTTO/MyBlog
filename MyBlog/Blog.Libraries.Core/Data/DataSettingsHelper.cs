
namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 数据源设置帮助
    /// </summary>
    public class DataSettingsHelper
    {

        /// <summary>
        /// 数据库数据设置是否有效
        /// </summary>
        private static bool? _databaseIsInstalled;

        /// <summary>
        /// 获取一个值,标识数据库数据设置是否有效
        /// </summary>
        public static bool DatabaseInstalled()
        {
            if (!_databaseIsInstalled.HasValue)
            {
                var manager = new DataSettingsManager();
                var settings = manager.LoadSettings();
                _databaseIsInstalled = settings != null && settings.IsValid();
            }
            return _databaseIsInstalled.Value;
        }

        /// <summary>
        /// 清除已知数据源数据设置状态
        /// </summary>
        public static void ResetCache()
        {
            _databaseIsInstalled = null;
        }

    }

}
