using DNG.Foundation.ORM.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;

namespace DNG.Foundation.ORM.DependencyInjection.Services
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