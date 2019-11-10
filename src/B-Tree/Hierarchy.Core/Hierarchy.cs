namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

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
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            if (this.nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var parentNode = this.nodesByValue[element];
            var childNode = new Node(child, parentNode);
            this.nodesByValue.Add(child, childNode);
            parentNode.Children.Add(childNode);
        }

        public void Remove(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var currentNode = this.nodesByValue[element];

            if (currentNode.Parent == null)
            {
                throw new InvalidOperationException();
            }

            if (currentNode.Children.Count != 0)
            {
                currentNode.Parent.Children.AddRange(currentNode.Children);

                foreach (var child in currentNode.Children)
                {
                    child.Parent = currentNode.Parent;
                }
            }

            currentNode.Parent.Children.Remove(currentNode);
            this.nodesByValue.Remove(element);
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.Contains(item))
            {
                throw new ArgumentException();
            }

            return this.nodesByValue[item].Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.Contains(item))
            {
                throw new ArgumentException();
            }

            var parent = this.nodesByValue[item].Parent;

            return parent == null ? default : parent.Value;
        }

        public bool Contains(T value)
        {
            if (this.nodesByValue.ContainsKey(value))
            {
                return true;
            }

            return false;
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            HashSet<T> result = new HashSet<T>(this.nodesByValue.Keys);
            result.IntersectWith(new HashSet<T>(other.nodesByValue.Keys));
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node> queue = new Queue<Node>();

            Node current = this.root;
            queue.Enqueue(current);

            while (queue.Count > 0)
            {
                current = queue.Dequeue();
                yield return current.Value;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

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