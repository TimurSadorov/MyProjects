using System;

namespace BTree
{
    static class ExtensionMethods
    {
        public static bool FindKeyThroughForeach<T>(this ITree<T> tree, T key)
            where T : IComparable
        {
            foreach (var keyInTree in tree)
                if (keyInTree.CompareTo(key) == 0)
                    return true;
            return false;
        }
    }
}
