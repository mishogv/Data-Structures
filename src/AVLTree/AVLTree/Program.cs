using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> avl = new AVL<int>();
        for (int i = 50; i < 100; i++)
        {
            avl.Insert(i);
        }

        avl.DeleteMin();

        avl.EachInOrder(Console.WriteLine);
    }
}
