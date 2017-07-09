using System;
using Acr.Settings;

namespace POAFGVApp
{
    public class PersonalSettings : IPersonalSettings
    {
        ISettings _settings { get; }

        public PersonalSettings(ISettings settings)
        {
            _settings = settings;
        }

        public void Set(string key, object TEntity)
        {
            try
            {
                _settings.Set(key, TEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Get(string key, Type type)
        {
            try
            {
                return _settings.Get(key, type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
