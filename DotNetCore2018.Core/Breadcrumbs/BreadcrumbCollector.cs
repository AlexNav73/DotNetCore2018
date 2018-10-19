using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DotNetCore2018.Core.Breadcrumbs
{
    public static class BreadcrumbCollector
    {
        public static BreadcrumbNode Collect(Assembly assembly)
        {
            var nodes = new List<BreadcrumbNode>();

            var typesWithBreadcrumbs = assembly.GetTypes()
                .Select(x => (x, x.GetCustomAttribute<BreadcrumbAttribute>()))
                .Where(x => x.Item2 != null)
                .ToArray();
            var (rootType, rootAttribute) = typesWithBreadcrumbs.SingleOrDefault(x => x.Item2.Parent == null);

            var rootNode = GetTypeRootNode(rootType, rootAttribute);
            nodes.Add(rootNode);
            foreach (var (t, attr) in typesWithBreadcrumbs.Where(x => x.Item2.Parent != null))
            {
                nodes.Add(GetTypeRootNode(t, attr));
            }
            LinkNodes(nodes, rootType);

            return rootNode;
        }

        private static void LinkNodes(List<BreadcrumbNode> nodes, Type rootType)
        {
            var registry = new Dictionary<Type, BreadcrumbNode>();
            var rootNode = nodes.Single(x => x.ParentType == null);
            var notLinkedNodes = nodes.Count - 1;

            registry.Add(rootType, rootNode);
            while (notLinkedNodes > 0)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    if (nodes[i].ParentType != null && 
                        nodes[i].Parent == null &&
                        registry.TryGetValue(nodes[i].ParentType, out var parent))
                    {
                        nodes[i].Parent = parent;
                        parent.Children.Add(nodes[i]);
                        registry.Add(nodes[i].Type, nodes[i]);
                        notLinkedNodes--;
                    }
                }
            }
        }

        private static BreadcrumbNode GetTypeRootNode(Type rootType, BreadcrumbAttribute rootAttribute)
        {
            var rootNode = new BreadcrumbNode() 
            { 
                Type = rootType,
                ParentType = rootAttribute.Parent,
                Name = rootAttribute.Name ?? rootType.Name.Replace("Controller", string.Empty)
            };

            rootNode.Children = rootType.GetMethods()
                .Select(x => (x, x.GetCustomAttribute<BreadcrumbAttribute>()))
                .Where(x => x.Item2 != null)
                .Select(x => new BreadcrumbNode() { Parent = rootNode, Name = x.Item2.Name ?? x.Item1.Name })
                .ToList();

            if (rootNode.Children.Count == 0)
            {
                rootNode.Children.Add(new BreadcrumbNode() { Parent = rootNode, Name = "Index"});
            }

            return rootNode;
        }
    }
}