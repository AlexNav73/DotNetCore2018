using System;
using System.Collections.Generic;

namespace DotNetCore2018.Core.Breadcrumbs
{
    public class BreadcrumbNode
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public Type ParentType { get; set; }
        public BreadcrumbNode Parent { get; set; }
        public List<BreadcrumbNode> Children { get; set; }
    }
}