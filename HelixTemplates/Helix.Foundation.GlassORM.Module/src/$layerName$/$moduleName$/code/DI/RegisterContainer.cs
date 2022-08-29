using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web;
using Glass.Mapper.Sc.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using System.Linq;

namespace $modulenamespace$.DI
{
    public class RegisterContainer : IServicesConfigurator
    {
        public void Configure(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ISitecoreService>(provider =>
            {
                var databaseName = Sitecore.Context.Database?.Name.ToLower();
                if (databaseName == null || databaseName == "core")
                    return new SitecoreService("master");
                return new SitecoreService(Sitecore.Context.Database);
            });

            serviceCollection.AddScoped<IRequestContext, RequestContext>();
            serviceCollection.AddScoped<IMvcContext, MvcContext>();
            serviceCollection.AddScoped<IGlassHtml, GlassHtml>();
        }
    }
}