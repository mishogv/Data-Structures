using System.Collections.Generic;

namespace Basic_Tree_Data_Structures
{
    interface ITreeService
    {
        IEnumerable<int> GetLongestPathLeftPriority(Tree<int> node);

        IEnumerable<IEnumerable<int>> GetAllPathsWithGivenSum(Tree<int> node, int sum);

        string GetTree(Tree<int> node, int indented = 0);

        IEnumerable<int> GetAllLeafNodes();

        IEnumerable<int> GetAllMiddleNodes();

        int GetLeftDeepestNode(Tree<int> node);

        Tree<int> GetTreeNodeByValue(int value);

        void AddEdge(int parent, int child);

        Tree<int> GetRootNode();
    }
}
