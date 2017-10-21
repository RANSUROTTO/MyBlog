using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Data.Domain.Configuration;

namespace Blog.Libraries.Services.Configuration
{

    public interface ISettingService
    {
        /// <summary>
        /// 插入设定
        /// </summary>
        /// <param name="setting">设定</param>
        /// <param name="clearCache">是否清除缓存</param>
        void InsertSetting(Setting setting, bool clearCache = true);

        /// <summary>
        /// 更新设定
        /// </summary>
        /// <param name="setting">设定</param>
        /// <param name="clearCache">是否清除缓存</param>
        void UpdateSetting(Setting setting, bool clearCache = true);

        /// <summary>
        /// 获取全部设定
        /// </summary>
        /// <returns>全部设定</returns>
        IList<Setting> GetAllSettings();

        /// <summary>
        /// 通过ID获取设定
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns></returns>
        Setting GetSettingById(long settingId);

        /// <summary>
        /// 删除设定
        /// </summary>
        /// <param name="setting">设定</param>
        void DeleteSetting(Setting setting);

        /// <summary>
        /// 删除多个设定
        /// </summary>
        /// <param name="settings">多个设定</param>
        void DeleteSettings(IList<Setting> settings);

        /// <summary>
        /// 通过key获取设定
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>设定</returns>
        Setting GetSettingByKey(string key);

        /// <summary>
        /// 通过key获取设定并转换为泛型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="defaultValue">设定为null时返回的默认值</param>
        /// <returns>T实例</returns>
        T GetSettingByKey<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// 设置指定key的设定值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="clearCache">是否清空缓存</param>
        void SetSetting<T>(string key, T value, bool clearCache = true);

        /// <summary>
        /// 查看某个设定属性是否存在
        /// </summary>
        /// <param name="settings">设定</param>
        /// <param name="keySelector">属性</param>
        /// <returns>结果</returns>
        bool SettingExists<T, TProType>(T settings, Expression<Func<T, TProType>> keySelector) where T : ISettings, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T LoadSetting<T>() where T : ISettings, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        void SaveSetting<T>(T settings) where T : ISettings, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="settings"></param>
        void DeleteSetting<T>(T settings) where T : ISettings, new();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TPropType"></typeparam>
        /// <param name="settings"></param>
        /// <param name="keySelector"></param>
        void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new();

        /// <summary>
        /// 清空设定缓存
        /// </summary>
        void ClearCache();

    }

}
