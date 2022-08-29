using Glass.Mapper.Sc.Configuration;
using Glass.Mapper.Sc.Configuration.Attributes;
using Sitecore.Data;
using System;

namespace $modulenamespace$
{
    public abstract partial class GlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Path)]
        public virtual string Path { get; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        public virtual string Name { get; set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        public virtual string DisplayName { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        public virtual string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        public virtual ID TemplateId { get; set; }
    }

    public partial interface IGlassBase
    {
        [SitecoreInfo(SitecoreInfoType.Path)]
        string Path { get; }

        [SitecoreInfo(SitecoreInfoType.Name)]
        string Name { get; set; }

        [SitecoreInfo(SitecoreInfoType.DisplayName)]
        string DisplayName { get; set; }

        [SitecoreInfo(SitecoreInfoType.Url)]
        string Url { get; set; }

        [SitecoreInfo(SitecoreInfoType.TemplateId)]
        ID TemplateId { get; set; }
    }
}