using System;
using System.Linq;
using DNG.Feature.PageContent.Models;
using DNG.Foundation.ORM.DependencyInjection.Attributes;
using DNG.Foundation.ORM.Services;
using DNG.Foundation.ORM;
using System.Web;

namespace DNG.Feature.PageContent.Services
{
    [Service(typeof(IPageContentService))]
    public class PageContentService : IPageContentService
    {
        private readonly IRenderingService _renderingService;
        private readonly IEntityService _entityService;
        public PageContentService(IRenderingService renderingService, IEntityService entityService)
        {
            _renderingService = renderingService;
            _entityService = entityService;
        }
        public PageContentViewModel CreateViewModel()
        {
            var model = new PageContentViewModel();
            var currentItem = _renderingService.GetPageContextItem<I_Page_Base>();
            if (currentItem != null)
            {
                model.PageTitle = currentItem.Page_Title;
                model.PageContent = new HtmlString(currentItem.Page_Content);
                model.PageSubDescription = new HtmlString(currentItem.Page_SubDescription);
                model.DemoForARM = new HtmlString(currentItem.DemoForARM);
            }

            return model;
        }
    }
}