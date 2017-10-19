using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Blog.Libraries.Core.Caching;
using Blog.Libraries.Core.Configuration;
using Blog.Libraries.Core.Data;
using Blog.Libraries.Core.Helper;
using Blog.Libraries.Data.Domain.Configuration;

namespace Blog.Libraries.Services.Configuration
{

    public class SettingService : ISettingService
    {

        #region Fields

        private const string AllSettingCacheKey = "ransurotto.cn.setting.cache.all";

        private readonly IRepository<Setting> _settingRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Constructor

        public SettingService(IRepository<Setting> settingRepository, ICacheManager cacheManager)
        {
            _settingRepository = settingRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Methods

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

            var settings = GetAllSettingCached();

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
            throw new NotImplementedException();
        }

        public bool SettingExists<T, TProType>(T setting, Expression<Func<T, TProType>> keySelector) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public T LoadSetting<T>() where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public void SaveSetting<T>(T settings) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public void DeleteSetting<T>(T settings) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public void DeleteSetting<T, TPropType>(T settings, Expression<Func<T, TPropType>> keySelector) where T : ISettings, new()
        {
            throw new NotImplementedException();
        }

        public void ClearCache()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Utilities

        public virtual IDictionary<string, Setting> GetAllSettingCached()
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
