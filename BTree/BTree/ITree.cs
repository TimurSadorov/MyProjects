using System.Collections.Generic;

namespace BTree
{
    public interface ITree<T> : IEnumerable<T>
    {
        bool Find(T item);
        void Add(T item);
        bool Remove(T item);
    }
}
