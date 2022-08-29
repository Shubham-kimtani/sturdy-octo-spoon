using System.Web.Mvc;
using $modulenamespace$.Services;

namespace $modulenamespace$.Controllers
{
    public class $modulename$Controller : Controller
    {
        private readonly I$modulename$Service _$modulename$Service;

          public $modulename$Controller(I$modulename$Service $modulename$Service)
          {
              _$modulename$Service = $modulename$Service;
          }
          public ActionResult Index()
          {
              var mediatorResponse = _$modulename$Service.CreateViewModel();
              return View(mediatorResponse);
          }
    }
}