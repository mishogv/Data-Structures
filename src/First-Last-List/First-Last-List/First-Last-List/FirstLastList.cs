using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{
    private readonly LinkedList<T> list = new LinkedList<T>();
    private readonly OrderedBag<LinkedListNode<T>> bag
        = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
    private readonly OrderedBag<LinkedListNode<T>> bagByDesc
        = new OrderedBag<LinkedListNode<T>>((x, y) => y.Value.CompareTo(x.Value));

    public int Count
    {
        get
        {
            return list.Count;
        }
    }

    public void Add(T element)
    {
        var node = new LinkedListNode<T>(element);
        list.AddLast(node);
        bag.Add(node);
        bagByDesc.Add(node);
    }

    public void Clear()
    {
        list.Clear();
        bag.Clear();
        bagByDesc.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        IsCountEnought(count);
        return list.Take(count);
    }

    public IEnumerable<T> Last(int count)
    {
        IsCountEnought(count);
        var currentElement = list.Last;
        for (int i = 0; i < count; i++)
        {
            yield return currentElement.Value;
            currentElement = currentElement.Previous;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        IsCountEnought(count);
        return bagByDesc.Take(count).Select(x => x.Value);
    }

    public IEnumerable<T> Min(int count)
    {
        IsCountEnought(count);
        return bag.Take(count).Select(x => x.Value);
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);
        foreach (var item in
            this.bag.Range(node, true, node, true))
        {
            this.list.Remove(item);
        }
        int count = this.bag.RemoveAllCopies(node);
        this.bagByDesc.RemoveAllCopies(node);
        return count;
    }

    private void IsCountEnought(int count)
    {
        if (count > this.list.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
