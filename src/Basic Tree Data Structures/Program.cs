using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Tree_Data_Structures
{
    public class Program
    {
        public static Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();

        public static void Main()
        {
            ReadTree();
            var sum = int.Parse(Console.ReadLine());
            //var s = int.Parse(Console.ReadLine());
            var root = GetRootNode();
            PrintTree(root);
            //PrintAllLeafNodes();
            //PrintAllMiddleNodes();
            //PrintLeftDeepestNode(root);
            //PrintAllPathsWithGivenSum(root, sum);
            //PrintLongestPathLeftPriority();
            //FindAllSubtreesWithGivenSum(root, sum);
        }

        private static void FindAllSubtreesWithGivenSum(Tree<int> node, int sum)
        {
            // TODO : Implement
            //var subtrees = new List<List<int>>();
            //FindAllSubtreesWithGivenSum(node, sum, subtrees, new List<int>());
            //Console.WriteLine($"Subtrees of sum {sum}:");
            //subtrees.ForEach(x => Console.WriteLine(string.Join(" ", x)));
        }

        private static void FindAllSubtreesWithGivenSum(Tree<int> node, int sum, List<List<int>> subtrees, List<int> helper)
        {
            // TODO : Implement
            //if (node == null)
            //{
            //    return;
            //}

            //helper.Add(node.Value);

            //if (helper.Sum() == sum)
            //{
            //    subtrees.Add(new List<int>(helper));
            //}

            ////if (node.Children.Count == 0)
            ////{
            ////    return;
            ////}

            //foreach (var child in node.Children)
            //{
            //    FindAllSubtreesWithGivenSum(child, sum, subtrees, helper);
            //}
        }

        public static void PrintLongestPathLeftPriority(Tree<int> node)
        {
            var longestPath = new List<int>();
            FindLongestPath(node, longestPath, new List<int>());
            Console.WriteLine($"Longest path: {string.Join(" ", longestPath)}");
        }

        private static void FindLongestPath(Tree<int> node, List<int> longestPath, List<int> currentPath)
        {
            if(node == null)
            {
                return;
            }

            currentPath.Add(node.Value);

            if (longestPath.Count < currentPath.Count)
            {
                longestPath.Clear();
                longestPath.AddRange(currentPath);
            }

            foreach (var child in node.Children)
            {
                FindLongestPath(child, longestPath, currentPath);
            }

            currentPath.RemoveAt(currentPath.Count - 1);
        }

        public static void PrintAllPathsWithGivenSum(Tree<int> node, int sum)
        {
            Console.WriteLine($"Paths of sum: {sum}");
            var leftPath = new List<int>();
            FindLeftPathWithGivenSum(node, sum, leftPath);
            Console.WriteLine(string.Join(" ", leftPath.Sum() == sum ? leftPath : new List<int>()));
            var rightPath = new List<int>();
            FindRightPathWithGivenSum(node, sum, rightPath);
            Console.WriteLine(string.Join(" ", rightPath.Sum() == sum ? rightPath : new List<int>()));
        }

        private static void FindLeftPathWithGivenSum(Tree<int> node, int sum, List<int> paths)
        {
            if (node == null || paths.Sum() >= sum)
            {
                return;
            }

            paths.Add(node.Value);
            FindLeftPathWithGivenSum(node.Children.FirstOrDefault(), sum, paths);
        }

        private static void FindRightPathWithGivenSum(Tree<int> node, int sum, List<int> paths)
        {
            if (node == null || paths.Sum() >= sum)
            {
                return;
            }

            paths.Add(node.Value);
            FindRightPathWithGivenSum(node.Children.LastOrDefault(), sum, paths);
        }

        public static void PrintTree<T>(Tree<T> node, int indented = 0)
        {
            if (node == null)
            {
                return;
            }

            Console.WriteLine($"{new string(' ', indented)}{node.Value}");

            foreach (var child in node.Children)
            {
                PrintTree(child, indented + 2);
            }
        }

        public static void PrintAllLeafNodes()
        {
            var allLeafNodes = nodeByValue.Values
                .Where(x => x.Children.Count == 0 && x.Parent != null)
                .OrderBy(x => x.Value)
                .Select(x => x.Value)
                .ToList();

            Console.WriteLine($"Leaf nodes: {string.Join(" ", allLeafNodes)}");
        }

        public static void PrintAllMiddleNodes()
        {
            var allLeafNodes = nodeByValue.Values
                .Where(x => x.Children.Count != 0 && x.Parent != null)
                .OrderBy(x => x.Value)
                .Select(x => x.Value)
                .ToList();

            Console.WriteLine($"Leaf nodes: {string.Join(" ", allLeafNodes)}");
        }

        public static void PrintLeftDeepestNode(Tree<int> node)
        {
            Console.WriteLine($"Deepest node: {GetLeftDeepestNode(node)}");
        }

        private static int GetLeftDeepestNode(Tree<int> node)
        {
            if (node == null)
            {
                return int.MinValue;
            }

            var deepestNode = GetLeftDeepestNode(node.Children.FirstOrDefault());

            if (deepestNode == int.MinValue)
            {
                deepestNode = node.Value;
            }

            return deepestNode;
        }

        public static Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public static void AddEdge(int parent, int child)
        {
            var parentNode = GetTreeNodeByValue(parent);
            var childNode = GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        public static void ReadTree()
        {
            var nodeCount = int.Parse(Console.ReadLine());
            for (int i = 0; i < nodeCount - 1; i++)
            {
                var edge = Console.ReadLine()
                    .Trim()
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                AddEdge(edge[0], edge[1]);
            }
        }

        public static Tree<int> GetRootNode()
            => nodeByValue.FirstOrDefault(x => x.Value.Parent == null).Value;
    }
}
