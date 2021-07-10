using System;
using System.Collections.Generic;

namespace BTree
{
    static class ExtensionMethods
    {
        public static int FindFirstIndexOrCountOfList<T>(this List<T> list, T item)
            where T: IComparable
        {
            var firstIndex = list.FindIndex(e => e.CompareTo(item) >= 0);
            return firstIndex == -1 ? list.Count : firstIndex;
        }
    }
}
