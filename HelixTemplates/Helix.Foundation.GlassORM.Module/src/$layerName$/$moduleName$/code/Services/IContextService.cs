using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace $moduleNamespace$.Services
{
    public interface IContextService
{
    T GetCurrentItem<T>() where T : class;

    object GetCurrentItem(GetKnownOptions options);

    T GetCurrentItem<T>(GetKnownOptions options) where T : class;

    T GetHomeItem<T>() where T : class;

    T GetHomeItem<T>(GetKnownOptions options) where T : class;

    T GetRootItem<T>() where T : class;

    T GetRootItem<T>(GetKnownOptions options) where T : class;

    Item ContextItem { get; }

    Item TenantItem { get; }
}
}