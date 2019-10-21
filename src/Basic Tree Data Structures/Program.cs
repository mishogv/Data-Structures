using System;
using System.Collections.Generic;
using System.Linq;

namespace Basic_Tree_Data_Structures
{
    public class Program
    {
        private static readonly ITreeService treeService = new TreeService(new Dictionary<int, Tree<int>>());

        public static void Main()
        {
            ReadTree();
            var sum = int.Parse(Console.ReadLine());
            //var s = int.Parse(Console.ReadLine());
            var root = treeService.GetRootNode();
            Console.WriteLine(treeService.GetTree(root));
            //PrintAllLeafNodes();
            //PrintAllMiddleNodes();
            //PrintLeftDeepestNode(root);
            //PrintAllPathsWithGivenSum(root, sum);
            //PrintLongestPathLeftPriority();
            //FindAllSubtreesWithGivenSum(root, sum);
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

                treeService.AddEdge(edge[0], edge[1]);
            }
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
    }
}
