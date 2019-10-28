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

            //          9
            //          7 19
            //          7 21
            //          7 14
            //          19 1
            //          19 12
            //          19 31
            //          14 23
            //          14 6
            //          43

            var result = new List<List<int>>();
            treeService.GetTreeWithGivenSum(root, sum, result);
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
    }
}
