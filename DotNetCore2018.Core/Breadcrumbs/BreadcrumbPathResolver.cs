using System;
using System.Collections.Generic;

namespace DotNetCore2018.Core.Breadcrumbs
{
    public static class BreadcrumbPathResolver
    {
        public static List<BreadcrumbNode> GetPath(BreadcrumbNode tree, string controller, string action)
        {
            var targetNode = FindNode(tree, controller, action);
            var path = new List<BreadcrumbNode>();
            while (targetNode != null)
            {
                path.Add(targetNode);
                targetNode = targetNode.Parent;
            }
            return path;
        }

        private static BreadcrumbNode FindNode(BreadcrumbNode node, string controller, string action)
        {
            if (node.Name.Equals(action, StringComparison.OrdinalIgnoreCase) &&
                node.Parent != null &&
                node.Parent.Name.Equals(controller, StringComparison.OrdinalIgnoreCase))
            {
                return node;
            }
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var targetNode = FindNode(child, controller, action);
                    if (targetNode != null)
                    {
                        return targetNode;
                    }
                }
            }
            return null;
        }
    }
}