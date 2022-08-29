using System.Collections.Generic;
using System.Web;

namespace DNG.Feature.PageContent.Models
{
    public class PageContentViewModel
    {
        public string PageTitle { get; set; }
        public IHtmlString PageContent { get; set; }
        public IHtmlString PageSubDescription { get; set; }
        public string PageHeader { get; set; }
        public IHtmlString DemoForARM { get; set; }

    }
}