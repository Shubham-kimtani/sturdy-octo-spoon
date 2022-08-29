using Sitecore.Data.Items;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using $moduleNamespace$.DependencyInjection.Attributes;
namespace $moduleNamespace$.Services
{
    
    [Service(typeof(IContextService))]
    public class ContextService : IContextService
    {
        private readonly IRequestContext _requestContext;
        private readonly IEntityService _entityService;

        public ContextService(IRequestContext requestContext, IEntityService entityService)
        {
            _requestContext = requestContext;
            _entityService = entityService;
        }

        public T GetCurrentItem<T>() where T : class
        {
            return _requestContext.GetContextItem<T>();
        }

        public object GetCurrentItem(GetKnownOptions options)
        {
            return _requestContext.GetContextItem(options);
        }

        public T GetCurrentItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetContextItem<T>(options);
        }

        public T GetHomeItem<T>() where T : class
        {
            return _requestContext.GetHomeItem<T>();
        }

        public T GetHomeItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetHomeItem<T>(options);
        }

        public T GetRootItem<T>() where T : class
        {
            return _requestContext.GetRootItem<T>();
        }

        public T GetRootItem<T>(GetKnownOptions options) where T : class
        {
            return _requestContext.GetRootItem<T>(options);
        }

        public Item ContextItem => _requestContext.ContextItem;

        public Item TenantItem => GetRootItem<Item>().Parent;
    }
}