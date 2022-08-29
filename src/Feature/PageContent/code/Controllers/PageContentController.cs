using System.Web.Mvc;
using DNG.Feature.PageContent.Services;

namespace DNG.Feature.PageContent.Controllers
{
    public class PageContentController : Controller
    {
        private readonly IPageContentService _PageContentService;

          public PageContentController(IPageContentService PageContentService)
          {
              _PageContentService = PageContentService;
          }
          public ActionResult Index()
          {
              var mediatorResponse = _PageContentService.CreateViewModel();
              return View(mediatorResponse);
          }
    }
}