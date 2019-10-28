using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Basic_Tree_Data_Structures
{
    public class TreeService : ITreeService
    {
        private readonly IDictionary<int, Tree<int>> nodeByValue;

        public TreeService(IDictionary<int, Tree<int>> valueAndNode)
        {
            this.nodeByValue = valueAndNode;
        }

        public int GetTreeWithGivenSum(Tree<int> node, int sum, List<List<int>> allSubtree)
        {
            int currentSum = node.Value;

            foreach (var child in node.Children)
            {
                currentSum += GetTreeWithGivenSum(child, sum, allSubtree);
            }

            if (sum == currentSum)
            {
                List<int> subtree = new List<int>();
                this.GetSubtree(node, subtree);
                allSubtree.Add(new List<int>(subtree));
            }

            return currentSum;
        }

        private void GetSubtree(Tree<int> node, List<int> result)
        {
            result.Add(node.Value);
            foreach (var child in node.Children)
            {
                this.GetSubtree(child, result);
            }
        }

        public IEnumerable<int> GetLongestPathLeftPriority(Tree<int> node)
        {
            var longestPath = new List<int>();
            FindLongestPath(node, longestPath, new List<int>());
            return longestPath;
        }

        private void FindLongestPath(Tree<int> node, List<int> longestPath, List<int> currentPath)
        {
            if (node == null)
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

        public IEnumerable<IEnumerable<int>> GetAllPathsWithGivenSum(Tree<int> node, int sum)
        {
            var allPaths = new List<List<int>>();
            allPaths.Add(new List<int>());
            FindLeftPathWithGivenSum(node, sum, allPaths[0]);
            allPaths[0] = allPaths[0].Sum() == sum ? allPaths[0] : new List<int>();
            allPaths.Add(new List<int>());
            allPaths[1] = allPaths[1].Sum() == sum ? allPaths[1] : new List<int>();

            return allPaths;
        }

        private void FindLeftPathWithGivenSum(Tree<int> node, int sum, List<int> paths)
        {
            if (node == null || paths.Sum() >= sum)
            {
                return;
            }

            paths.Add(node.Value);
            FindLeftPathWithGivenSum(node.Children.FirstOrDefault(), sum, paths);
        }

        private void FindRightPathWithGivenSum(Tree<int> node, int sum, List<int> paths)
        {
            if (node == null || paths.Sum() >= sum)
            {
                return;
            }

            paths.Add(node.Value);
            FindRightPathWithGivenSum(node.Children.LastOrDefault(), sum, paths);
        }

        public string GetTree(Tree<int> node, int indented = 0)
        {
            var result = string.Empty;
            if (node == null)
            {
                return string.Empty;
            }

            result += Environment.NewLine + ($"{new string(' ', indented)}{node.Value}");

            foreach (var child in node.Children)
            {
                result += GetTree(child, indented + 2);
            }

            return result;
        }

        public IEnumerable<int> GetAllLeafNodes()
            => this.nodeByValue.Values
                .Where(x => x.Children.Count == 0 && x.Parent != null)
                .OrderBy(x => x.Value)
                .Select(x => x.Value)
                .ToList();

        public IEnumerable<int> GetAllMiddleNodes()
            => this.nodeByValue.Values
                .Where(x => x.Children.Count != 0 && x.Parent != null)
                .OrderBy(x => x.Value)
                .Select(x => x.Value);

        public int GetLeftDeepestNode(Tree<int> node)
            => this.FindLeftDeepestNode(node);

        private int FindLeftDeepestNode(Tree<int> node)
        {
            if (node == null)
            {
                return int.MinValue;
            }

            var deepestNode = FindLeftDeepestNode(node.Children.FirstOrDefault());

            if (deepestNode == int.MinValue)
            {
                deepestNode = node.Value;
            }

            return deepestNode;
        }

        public Tree<int> GetTreeNodeByValue(int value)
        {
            if (!nodeByValue.ContainsKey(value))
            {
                nodeByValue[value] = new Tree<int>(value);
            }

            return nodeByValue[value];
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = this.GetTreeNodeByValue(parent);
            var childNode = this.GetTreeNodeByValue(child);

            parentNode.Children.Add(childNode);
            childNode.Parent = parentNode;
        }

        public Tree<int> GetRootNode()
            => this.nodeByValue.FirstOrDefault(x => x.Value.Parent == null).Value;
    }
}
