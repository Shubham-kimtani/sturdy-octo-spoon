using Glass.Mapper.Sc.Configuration.Attributes;
using Glass.Mapper.Sc.Pipelines.GetGlassLoaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace $modulenamespace$.DI
{
    public class FeatureGlassLoader : GetGlassLoadersProcessor
    {
        public override void Process(GetGlassLoadersPipelineArgs args)
        {
            var loader = new SitecoreAttributeConfigurationLoader("$modulenamespace$");
            args.Loaders.Add(loader);
        }
    }
}