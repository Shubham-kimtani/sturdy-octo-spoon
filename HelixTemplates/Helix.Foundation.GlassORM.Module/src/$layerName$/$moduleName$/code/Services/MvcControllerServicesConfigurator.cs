using $moduleNamespace$.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace $moduleNamespace$.DependencyInjection.Services
{
    public class MvcControllerServicesConfigurator : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMvcControllers("*.Feature.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Feature.*");
            serviceCollection.AddClassesWithServiceAttribute("*.Foundation.*");
        }
    }
}