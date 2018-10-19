using System;

namespace DotNetCore2018.Core.Breadcrumbs
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class BreadcrumbAttribute : Attribute
    {
        public string Name { get; set; }
        public Type Parent { get; set; }

        public BreadcrumbAttribute() 
        {
        }

        public BreadcrumbAttribute(string name)
        {
            Name = name;
        }
    }
}