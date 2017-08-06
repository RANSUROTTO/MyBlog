using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Extension;

namespace Blog.Libraries.Core.Data
{

    /// <summary>
    /// 数据源设置管理
    /// </summary>
    public class DataSettingsManager
    {

        #region Field

        /// <summary>
        /// 键/值 字符串分隔符
        /// </summary>
        protected const char separator = ':';

        /// <summary>
        /// 数据源设置 信息存储文件
        /// </summary>
        protected const string filename = "db.config";

        #endregion

        #region Methods

        /// <summary>
        /// 将字符串解析为数据源设置实例
        /// </summary>
        /// <param name="settingString">数据源设置字符串</param>
        /// <returns>数据源设置实例</returns>
        protected virtual DataSettings ParseSettins(string settingString)
        {
            var shellSettings = new DataSettings();
            if (string.IsNullOrEmpty(settingString))
                return shellSettings;

            var settings = new List<string>();
            using (var reader = new StringReader(settingString))
            {
                string str;
                while (!string.IsNullOrEmpty((str = reader.ReadLine())))
                {
                    settings.Add(str);
                }
            }

            foreach (var setting in settings)
            {
                var separatorIndex = setting.IndexOf(separator);
                if (separatorIndex == -1)
                    continue;

                string key = setting.Substring(0, separatorIndex).Trim();
                string value = setting.Substring(separatorIndex + 1).Trim();

                PropertyInfo property = typeof(DataSettings).GetProperty(key);
                if (property != null && property.CanWrite)
                {
                    property.SetValue(shellSettings, value);
                }
                else
                {
                    shellSettings.RawDataSettings.Add(key, value);
                }
            }

            return shellSettings;
        }

        /// <summary>
        /// 将数据源设置实例解析为字符串
        /// </summary>
        /// <param name="shellSettings">数据源设置实例</param>
        /// <returns>数据源设置字符串</returns>
        protected virtual string ComposeSettings(DataSettings shellSettings)
        {
            if (shellSettings == null)
                return string.Empty;

            var settingString = new StringBuilder();
            foreach (var propertiy in typeof(DataSettings).GetProperties())
            {
                if (propertiy.PropertyType.IsBasicTypeOrString())
                {
                    settingString.AppendFormat("{0}:{1}{2}"
                        , propertiy.Name
                        , propertiy.GetValue(shellSettings),
                        Environment.NewLine);
                }
            }

            return settingString.ToString();
        }

        #endregion

    }

}
