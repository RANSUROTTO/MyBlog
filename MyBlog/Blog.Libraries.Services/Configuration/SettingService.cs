using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Data.Domain.Configuration;

namespace Blog.Libraries.Services.Configuration
{

    public partial class SettingService : ISettingService
    {

        #region Fields

        private const string AllSettingCacheKey = "ransurotto.com.setting.all";
        private const string SettingsPatternKey = "ransurotto.com.setting.";

        private readonly IRepository<Setting> _settingRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public SettingService(IRepository<Setting> settingRepository, ICacheManager cacheManager) : base(settingRepository)
        {
            _settingRepository = settingRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void InsertSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Insert(setting);

            if (clearCache)
                ClearCache();
        }

        public void UpdateSetting(Setting setting, bool clearCache = true)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Update(setting);

            if (clearCache)
                ClearCache();
        }

        public IList<Setting> GetAllSettings()
        {
            var query = from s in _settingRepository.Table
                        orderby s.Name
                        select s;
            return query.ToList();
        }

        public Setting GetSettingById(long settingId)
        {
            var query = from s in _settingRepository.Table
                        where s.Id == settingId
                        select s;
            return query.FirstOrDefault();
        }

        public void DeleteSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Delete(setting);
        }

        public void DeleteSettings(IList<Setting> settings)
        {
            if (settings == null)
                throw new ArgumentNullException("settings");

            _settingRepository.Delete(settings);
        }

        public Setting GetSettingByKey(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentException("key is null or empty");

            var settings = GetAllSettingsByCached();

            if (settings.ContainsKey(key))
            {
                if (settings[key] != null)
                    return this.GetSettingById(settings[key].Id);
            }
            return null;
        }

        public T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            var setting = GetSettingByKey(key);

            if (setting != null)
                return CommonHelper.To<T>(setting);

            return defaultValue;
        }

        public void SetSetting<T>(string key, T value, bool clearCache = true)
        {
            if (key == null)
                throw new ArgumentNullException("key");

            string valueStr = TypeDescriptor.GetConverter(typeof(T)).ConvertToInvariantString(value);

            var allSettings = GetAllSettingsByCached();
            var settingForCaching = allSettings.ContainsKey(key) ?
                allSettings[key] : null;

            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                setting.Value = valueStr;
                UpdateSetting(setting, clearCache);
            }
            else
            {
                //insert
                var setting = new Setting
                {
                    Name = key,
                    Value = valueStr
                };
                InsertSetting(setting, clearCache);
            }
        }

        public bool SettingExists<T, TProType>(T settings, Expression<Func<T, TProType>> keySelector) where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);

            var setting = GetSettingByKey(key);
            return setting != null;
        }

        public T LoadSetting<T>() where T : ISettings, new()
        {
            //创建设定类的实例
            var settings = Activator.CreateInstance<T>();

            //为每个属性加载值
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                var key = typeof(T).Name + "." + prop.Name;
                var setting = GetSettingByKey<string>(key);
                if (setting == null)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).IsValid(setting))
                    continue;

                object value = TypeDescriptor.GetConverter(prop.PropertyType).ConvertFromInvariantString(setting);
                prop.SetValue(settings, value, null);
            }

            return settings;
        }

        public void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            foreach (var prop in typeof(T).GetProperties())
            {
                if (!prop.CanRead || !prop.CanWrite)
                    continue;

                if (!TypeDescriptor.GetConverter(prop.PropertyType).CanConvertFrom(typeof(string)))
                    continue;

                string key = typeof(T).Name + "." + prop.Name;

                dynamic value = prop.GetValue(settings, null);
                if (value != null)
                    SetSetting(key, value, false);
                else
                    SetSetting(key, "", false);
            }
            ClearCache();
        }

        public void DeleteSetting<T>(T settings) where T : ISettings, new()
        {
            var settingsToDelete = new List<Setting>();
            var allSettings = GetAllSettings();
            foreach (var prop in typeof(T).GetProperties())
            {
                string key = typeof(T).Name + "." + prop.Name;
                settingsToDelete.AddRange(allSettings.Where(x => x.Name.Equals(key, StringComparison.InvariantCultureIgnoreCase)));
            }

            DeleteSettings(settingsToDelete);
        }

        public void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            string key = settings.GetSettingKey(keySelector);
            key = key.Trim().ToLowerInvariant();

            var allSettings = GetAllSettingsByCached();
            var settingForCaching = allSettings.ContainsKey(key) ? allSettings[key] : null;
            if (settingForCaching != null)
            {
                //update
                var setting = GetSettingById(settingForCaching.Id);
                DeleteSetting(setting);
            }
        }

        public void ClearCache()
        {
            _cacheManager.RemoveByPattern(SettingsPatternKey);
        }

        #endregion

        #region Utilities

        public virtual IDictionary<string, Setting> GetAllSettingsByCached()
        {
            var cacheKey = string.Format(AllSettingCacheKey);
            return _cacheManager.Get(cacheKey, () =>
            {
                var settings = this.GetAllSettings();
                var dictionary = new Dictionary<string, Setting>();

                foreach (var setting in settings)
                {
                    if (!dictionary.ContainsKey(setting.Name))
                    {
                        dictionary.Add(setting.Name, setting);
                    }
                }
                return dictionary;
            });
        }

        #endregion

    }

}
