namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node root;
        private Dictionary<T, Node> nodesByValue;

        public Hierarchy(T root)
        {
            this.root = new Node(root);
            this.nodesByValue = new Dictionary<T, Node>
            {
                { root, this.root }
            };
        }

        public int Count
        {
            get => this.nodesByValue.Count;
        }

        public void Add(T element, T child)
        {
            throw new NotImplementedException();
        }

        public void Remove(T element)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetChildren(T item)
        {
            throw new NotImplementedException();
        }

        public T GetParent(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private class Node
        {
            public Node(T value, Node parent = null)
            {
                this.Value = value;
                this.Children = new List<Node>();
                this.Parent = parent;
            }

            public Node Parent { get; set; }
            public T Value { get; set; }
            public List<Node> Children { get; set; }
        }
    }
}