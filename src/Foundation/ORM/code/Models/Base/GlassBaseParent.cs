using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNG.Foundation.ORM
{
    public class GlassBaseParent: GlassBase
    {
        [SitecoreChildren]
        public virtual IEnumerable<Item> Children { get; set; }
    }
}