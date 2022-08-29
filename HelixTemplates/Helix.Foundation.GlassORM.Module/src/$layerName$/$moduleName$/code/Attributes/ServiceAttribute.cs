using System;

namespace $modulenamespace$.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ServiceAttribute : Attribute
    {
        public ServiceAttribute()
        {
        }

        public ServiceAttribute(Type serviceType)
        {
            ServiceType = serviceType;
        }

        public LifeTime Lifetime { get; set; } = LifeTime.Transient;
        public Type ServiceType { get; set; }
    }
}