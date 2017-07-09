using System;
namespace POAFGVApp
{
    public interface IPersonalSettings
    {
        void Set(string key, object TEntity);
        object Get(string key, Type type);
    }
}
