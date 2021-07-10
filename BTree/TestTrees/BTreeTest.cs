using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace BTree
{
    [TestFixture]
    public class BTreeTest
    {
        private readonly int degreeOfTree = 3;

        private static object[] keyInEmptyTree = new object[]
        {
            new object[] {new int[] {}, 2, false},
            new object[] {new int[] {}, 10, false},
        };
        private static object[] keyInTreeWithOneKey = new object[]
        {
            new object[] {new int[] {33}, 33, true},
            new object[] {new int[] {8}, 0, false}
        };
        private static object[] keyInRoot = new object[]
        {
            new object[] { new int[] { 10, 20, 30 }, 20, true },
            new object[] { new int[] {3, 1, 8}, 5, false}
        };
        private static object[] keyInLeaf = new object[]
        {
            new object[] {new int[] {9, 7, 2, 10, 5, 3}, 7, true},
            new object[] {new int[] { 9, 7, 2, 10, 5, 3 }, 20, false }
        };
        private static object[] keyInInternalNode = new object[]
        {
            new object[] {Enumerable.Range(0, 22), 3, true},
            new object[] {Enumerable.Range(10, 32), 21, true}
        };

        [TestCaseSource("keyInEmptyTree")]
        [TestCaseSource("keyInTreeWithOneKey")]
        [TestCaseSource("keyInRoot")]
        [TestCaseSource("keyInLeaf")]
        [TestCaseSource("keyInInternalNode")]
        public void FindTest(IEnumerable<int> elements, int requiredElement, bool expectedResult)
        {
            var tree = new BTree<int>(3, elements);
            Assert.AreEqual(expectedResult, tree.Find(requiredElement));
        }

        [Test]
        public void AddKeyInEmptyTree()
        {
            TestToAdd(new int[] {  }, 51, 0, 0, 1);
            TestToAdd(new int[] {  }, 1, 0, 0, 1);
        }

        [Test]
        public void AddKeyWithSplittingRoot()
        {
            TestToAdd(Enumerable.Range(0, 5), 10, 5, 1, 2);
            TestToAdd(Enumerable.Range(3, 5), 5, 5, 1, 2);
            TestToAdd(Enumerable.Range(0, 18), 2, 18, 2, 3);
        }

        [Test]
        public void AddKeyWithSplittingLeaf()
        {
            TestToAdd(Enumerable.Range(0, 8), 10, 8, 2, 2);
            TestToAdd(Enumerable.Range(0, 20), 21, 20, 3, 3);
        }

        [Test]
        public void AddKeyWithSplittingInternalNode()
        {
            TestToAdd(Enumerable.Range(0, 26), 26, 26, 3, 3);
            TestToAdd(Enumerable.Range(1, 16).Concat(Enumerable.Range(20, 11)), 17, 27, 3, 3);
        }

        private void TestToAdd(IEnumerable<int> source, int insertedKey,
            int countBeforeInsertion, int heightBeforeInsertion, int heightAfterInsertion)
        {
            var tree = new BTree<int>(degreeOfTree, source);
            Assert.AreEqual(countBeforeInsertion, tree.Count);
            Assert.AreEqual(heightBeforeInsertion, tree.TreeHeight);
            tree.Add(insertedKey);
            Assert.AreEqual(countBeforeInsertion + 1, tree.Count);
            Assert.AreEqual(heightAfterInsertion, tree.TreeHeight);
            Assert.AreEqual(true, tree.FindKeyThroughForeach(insertedKey));
        }

        [Test]
        public void RemoveKeyInEmptyTree()
        {
            TestToRemove(new int[] { }, 100, 0, 0, 0, false);
        }

        [Test]
        public void RemoveKeyInTreeWithOneKey()
        {
            TestToRemove(new int[] { 30 }, 30, 1, 1, 0, true);
            TestToRemove(new int[] { 23 }, 55, 1, 1, 1, false);
        }

        [Test]
        public void RemoveKeyInLeaf()
        {
            TestToRemove(new int[] { 10, 3, 5, 8, 9, 30 }, 9, 6, 2, 2, true);
            TestToRemove(new int[] { 10, 3, 5, 8, 9, 30 }, 7, 6, 2, 2, false);
        }

        [Test]
        public void RemoveKeyInInternalNodeWithMoveKeyLeveldown()
        {
            TestToRemove(Enumerable.Range(0, 23), 17, 23, 3, 3, true);
        }

        [Test]
        public void RemoveKeyInInternalNodeWithMerging()
        {
            TestToRemove(Enumerable.Range(1, 21), 12, 21, 3, 3, true);
        }

        [Test]
        public void RemoveKeyWithMergingAndCreatingNewRoot()
        {
            TestToRemove(Enumerable.Range(1, 18).Concat(Enumerable.Repeat(7, 1)), 6, 19, 3, 2, true);
            TestToRemove(Enumerable.Range(1, 18).Concat(Enumerable.Repeat(8, 1)), 9, 19, 3, 2, true);
        }

        [Test]
        public void RemoveKeyIsImpossible()
        {
            TestToRemove(new int[] { 10, 59, 1, -3, 5, 90 }, 100, 6, 2, 2, false);
            TestToRemove(Enumerable.Range(1, 25), 30, 25, 3, 3, false);
        }

        private void TestToRemove(IEnumerable<int> source, int removedKey,
            int countBeforeRemoving, int heightBeforeRemoving, int heightAfterRemoving, bool result)
        {
            var tree = new BTree<int>(degreeOfTree, source);
            Assert.AreEqual(countBeforeRemoving, tree.Count);
            Assert.AreEqual(heightBeforeRemoving, tree.TreeHeight);
            Assert.AreEqual(result, tree.Remove(removedKey));
            Assert.AreEqual(result ? countBeforeRemoving - 1 : countBeforeRemoving, tree.Count);
            Assert.AreEqual(heightAfterRemoving, tree.TreeHeight);
            Assert.AreEqual(false, tree.FindKeyThroughForeach(removedKey));
        }
   }
}
