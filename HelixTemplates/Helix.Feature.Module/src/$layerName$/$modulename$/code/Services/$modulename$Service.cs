using System;
using System.Linq;
using $modulenamespace$.Models;
//using $specifiedsolutionname$.Foundation.{{ORM project name}}.DependencyInjection.Attributes;
//using $specifiedsolutionname$.Foundation.{{ORM project name}}.Services;
//using $specifiedsolutionname$.Foundation.{{ORM project name}};

namespace $modulenamespace$.Services
{
    [Service(typeof(I$modulename$Service))]
    public class $modulename$Service : I$modulename$Service
    {
        private readonly IRenderingService _renderingService;
        private readonly IEntityService _entityService;
        public $modulename$Service(IRenderingService renderingService, IEntityService entityService)
        {
            _renderingService = renderingService;
            _entityService = entityService;
        }
        public $modulename$ViewModel CreateViewModel()
        {
            var model = new $modulename$ViewModel();
            return model;
        }
    }
}