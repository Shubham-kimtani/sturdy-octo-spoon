using DNG.Feature.PageContent.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DNG.Feature.PageContent.Controllers
{
    public class BuggyBitsController : Controller
    {
        private readonly IBuggyBitsService _buggyBitsService;

        public BuggyBitsController(IBuggyBitsService buggyBitsService)
        {
            _buggyBitsService = buggyBitsService;
        }

        public ActionResult Index()
        {
            var mediatorResponse = _buggyBitsService.Execute();            
            return PartialView("~/Views/BuggyBits/Index.cshtml",mediatorResponse);
        }
    }
}