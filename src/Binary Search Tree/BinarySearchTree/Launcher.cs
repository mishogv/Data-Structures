namespace Launcher
{
    using System;

    public class Launcher
    {
        public static void Main(string[] args)
        {
            BinarySearchTree<int> bst = new BinarySearchTree<int>();

            bst.Insert(10);
            bst.Insert(5);
            bst.Insert(3);
            bst.Insert(1);
            bst.Insert(4);
            bst.Insert(8);
            bst.Insert(9);
            bst.Insert(37);
            bst.Insert(39);
            bst.Insert(45);

            bst.EachInOrder(Console.WriteLine);

            var count = bst.Count();

            Console.WriteLine(count);
        }
    }
}