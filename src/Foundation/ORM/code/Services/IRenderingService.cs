using Glass.Mapper.Sc;
using Sitecore.Data.Items;

namespace DNG.Foundation.ORM.Services
{
    public interface IRenderingService
    {
        T GetDataSourceItem<T>(GetKnownOptions options) where T : class;
        T GetDataSourceItem<T>() where T : class;
        T GetPageContextItem<T>(GetKnownOptions options) where T : class;
        T GetPageContextItem<T>() where T : class;
        T GetRenderingItem<T>(GetKnownOptions options) where T : class;
        T GetRenderingItem<T>() where T : class;
        T GetRenderingParameters<T>() where T : class;
        bool HasDataSource { get; }
        Item DataSourceItem { get; }
        string RenderingParameters { get; }
    }
}
