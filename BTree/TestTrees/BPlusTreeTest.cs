using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BTree
{
    [TestFixture]
    public class BPlusTreeTest
    {
        private readonly int degreeOfTree = 3;

        [Test]
        public void FindKeyInEmptyTree()
        {
            TestToFind(null, 3, false);
            TestToFind(null, 10, false);
        }

        [Test]
        public void FindKeyInTreeWithHeightOne()
        {
            TestToFind(new int[] { 10, 3, 7, 9, 2 }, 9, true);
            TestToFind(new int[] { 2, 5, 9 }, 1, false);
        }

        [Test]
        public void FindKeyThatHasNoCopies()
        {
            TestToFind(Enumerable.Range(1, 6), 2, true);
            TestToFind(Enumerable.Range(1, 21), 15, true);
            TestToFind(Enumerable.Range(1, 10), -1, false);
        }

        [Test]
        public void FindKeyThatHasCopies()
        {
            TestToFind(Enumerable.Range(10, 10), 13, true);
            TestToFind(Enumerable.Range(1, 21), 13, true);
            TestToFind(Enumerable.Range(1, 21), 4, true);
        }

        private void TestToFind(IEnumerable<int> source, int requiredElement, bool result)
        {
            var tree = new BPlusTree<int>(degreeOfTree, source);
            Assert.AreEqual(result, tree.Find(requiredElement));
        }

        [Test]
        public void AddKeyInEmptyTree()
        {
            TestToAdd(new int[] { }, 2);
        }

        [Test]
        public void AddKeyInTreeWithHeightOne()
        {
            TestToAdd(new int[] { 10, 20, 2 }, 5);
            TestToAdd(new int[] { 3, 7, 11, 1 }, 4);
        }

        [Test]
        public void AddKeyInFilledRoot()
        {
            TestToAdd(new int[] { 10, 30, 2, 58, 1 }, 9);
            TestToAdd(new int[] { 20, 34, 2, 56, 78 }, 10);
        }

        [Test]
        public void AddKeyWithoutAddingCopy()
        {
            TestToAdd(new int[] { 3, 8, 12, 6, 1, 10, 4 }, 4);
        }

        [Test]
        public void AddKeyWithAddingCopy()
        {
            TestToAdd(new int[] { 4, 1, 13, 9, 40, 23, 32, 37 }, 5);
        }

        [Test]
        public void AddKeyWithRecursively()
        {
            TestToAdd(Enumerable.Range(1, 20), 24);
        }

        private void TestToAdd(IEnumerable<int> source, int insertedKey)
        {
            var tree = new BPlusTree<int>(degreeOfTree, source);
            tree.Add(insertedKey);
            Assert.AreEqual(true, tree.FindKeyThroughForeach(insertedKey));
        }

        [Test]
        public void RemoveFromEmptyTree()
        {
            TestToRemove(new int[] { }, new (int, bool)[] { (2, false) });
        }

        [Test]
        public void RemoveFromRoot()
        {
            TestToRemove(new int[] { 5, 2, 6 }, new (int, bool)[] { (5, true) });
            TestToRemove(new int[] { 0 }, new (int, bool)[] { (0, true) });
        }

        [Test]
        public void RemoveKeyThatHasNoCopy()
        {
            TestToRemove(new int[] { 3, 5, 8, 1, 4, 10 }, new (int, bool)[] { (3, true) });
        }

        [Test]
        public void RemoveKeyThatHasCopy()
        {
            TestToRemove(Enumerable.Range(5, 9), new (int, bool)[] { (8, true) });
            TestToRemove(Enumerable.Range(10, 12), new (int, bool)[] { (16, true) });
        }

        [Test]
        public void RemoveKeyWithCreatingNewRoot()
        {
            TestToRemove(Enumerable.Range(0, 6), new (int, bool)[] { (5, true), (3, true) });
        }

        [Test]
        public void RemoveKeyAndRecursively()
        {
            TestToRemove(Enumerable.Range(1, 21), new (int, bool)[] { (13, true), (15, true), (14, true) });
        }

        private void TestToRemove(IEnumerable<int> source, IEnumerable<(int, bool)> removedKeysWithResults)
        {
            var tree = new BPlusTree<int>(degreeOfTree, source);
            foreach (var keyAndResultForHim in removedKeysWithResults)
            {
                Assert.AreEqual(keyAndResultForHim.Item2, tree.Remove(keyAndResultForHim.Item1));
                Assert.AreEqual(false, tree.FindKeyThroughForeach(keyAndResultForHim.Item1));
            }
        }
    }
}
