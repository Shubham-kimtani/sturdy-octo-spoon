using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Pipelines.GetGlassLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DNG.Foundation.ORM.DI
{
    public class FeatureGlassLoader : GetGlassLoadersProcessor
    {
        public override void Process(GetGlassLoadersPipelineArgs args)
        {
            var loader = new SitecoreAttributeConfigurationLoader("DNG.Foundation.ORM");
            args.Loaders.Add(loader);
        }
    }
}