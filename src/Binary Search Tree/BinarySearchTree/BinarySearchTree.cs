using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T:IComparable
{
    private Node root;

    public BinarySearchTree()
    {

    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void Delete(T element)
    {
        if (this.Count(this.root) == 0 || !this.Contains(element))
        {
            throw new InvalidOperationException();
        }

        this.root = this.Delete(this.root, element);
    }

    private Node Delete(Node node, T element)
    {
        if (node == null)
        {
            return null;
        }

        var compare = node.Value.CompareTo(element);

        if (compare > 0)
        {
            node.Left = this.Delete(node.Left, element);
        }
        else if (compare < 0)
        {
            node.Right = this.Delete(node.Right, element);
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            if (node.Right == null)
            {
                return node.Left;
            }

            var leftMost = this.LeftMost(node);

            node.Value = leftMost.Value;
            node.Right = this.Delete(node.Right, leftMost.Value);
        }

        return node;
    }

    private Node LeftMost(Node node)
    {
        Node current = node;

        while (current.Left != null)
        {
            current = current.Left;
        }

        return current;
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        Node current = this.root;
        Node parent = null;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        if (parent == null)
        {
            this.root = this.root.Left;
        }
        else
        {
            parent.Right = current.Left;
        }
    }


    public int Count()
        => this.Count(this.root);

    private int Count(Node root)
    {
        if (root == null)
        {
            return 0;
        }

        var count = 1;

        if (root.Left != null)
        {
            count += this.Count(root.Left);
        }

        if (root.Right != null)
        {
            count += this.Count(root.Right);
        }

        return count;
    }

    public int Rank(T element)
        => this.Rank(element, this.root);

    private int Rank(T element, Node node)
    {
        if (node == null)
        {
            return 0;
        }

        var compare = node.Value.CompareTo(element);

        if (compare > 0)
        {
            return this.Rank(element, node.Left);
        }

        if (compare < 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
        }

        return this.Count(node.Left);
    }

    public T Select(int rank)
    {
        var node = this.Select(rank, this.root);
        return node != null ? node.Value : throw new InvalidOperationException();
    }

    private Node Select(int rank, Node node)
    {
        if (node == null)
        {
            return null;
        }

        var leftCount = this.Count(node.Left);

        if (leftCount.CompareTo(rank) > 0)
        {
            return this.Select(rank, node.Left);
        }
        else if (leftCount.CompareTo(rank) < 0)
        {
            return this.Select(rank - (leftCount + 1), node.Right);
        }

        return node;
    }

    public T Ceiling(T element)
        => this.Select(this.Rank(element) + 1);

    public T Floor(T element)
        => this.Select(this.Rank(element) - 1);

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }
    
    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; internal set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}