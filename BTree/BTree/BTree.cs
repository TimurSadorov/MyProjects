using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BTree
{
    public class BTree<T> : IEnumerable<T>, ITree<T>
        where T : IComparable
    {
        class Node<TNode>
        {
            public Node(bool isLeaf, IEnumerable<TNode> keys = null)
            {
                IsLeaf = isLeaf;
                Keys = keys == null ? new List<TNode>() : new List<TNode>(keys);
                if (!isLeaf)
                    Children = new List<Node<TNode>>();
            }

            public bool IsLeaf { get; }
            public List<Node<TNode>> Children { get; set; }
            public List<TNode> Keys { get; set; }
        }

        private Node<T> root;
        public int DegreeOfTree { get; }
        private readonly int maxAmountOfKeys;

        public int TreeHeight { get; private set; }
        public int Count { get; private set; }

        public BTree(int degreeOfTree, IEnumerable<T> source = null)
        {
            DegreeOfTree = degreeOfTree;
            maxAmountOfKeys = 2 * degreeOfTree - 1;
            if (source != null)
                foreach (var e in source)
                    this.Add(e);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null)
                yield break;
            foreach (var key in GetNextElements(root))
                yield return key;
        }

        private IEnumerable<T> GetNextElements(Node<T> nextNode)
        {
            if (nextNode.IsLeaf)
            {
                foreach (var key in nextNode.Keys)
                    yield return key;
                yield break;
            }
            for (int i = 0; i < nextNode.Children.Count; i++)
            {
                foreach (var keyInChild in GetNextElements(nextNode.Children[i]))
                    yield return keyInChild;
                if (i < nextNode.Keys.Count)
                    yield return nextNode.Keys[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (root == null)
            {
                root = new Node<T>(true, Enumerable.Repeat(item, 1));
                Count++;
                TreeHeight++;
                return;
            }
            var (leafForInsert, indexOfInsert) = FindLeafAndIndexOfInsert(item);
            leafForInsert.Keys.Insert(indexOfInsert, item);
            Count++;
        }

        private (Node<T>, int) FindLeafAndIndexOfInsert(T item)
        {
            if (root.Keys.Count == maxAmountOfKeys)
                BreakUpNode(root);
            var leafForInsert = FindLeafForInsert(item);
            var indexOfInsert = leafForInsert.Keys.FindLastIndex(key => item.CompareTo(key) > 0) + 1;
            return (leafForInsert, indexOfInsert);
        }

        private Node<T> FindLeafForInsert(T item)
        {
            var currentNode = root;
            while (!currentNode.IsLeaf)
            {
                var indexOfNodeInParent = currentNode.Keys.FindLastIndex(key => item.CompareTo(key) > 0) + 1;
                var child = currentNode.Children[indexOfNodeInParent];
                if (child.Keys.Count == maxAmountOfKeys)
                {
                    BreakUpNode(child, indexOfNodeInParent, currentNode, false);
                    currentNode = ChooseBetweenTwoNewChildren(item, currentNode, indexOfNodeInParent);
                }
                else
                    currentNode = currentNode.Children[indexOfNodeInParent];
            }
            return currentNode;
        }

        private Node<T> ChooseBetweenTwoNewChildren(T item,
            Node<T> currentNode, int indexOfNewKey)
        {
            if (item.CompareTo(currentNode.Keys[indexOfNewKey]) < 0)
                return currentNode.Children[indexOfNewKey];
            return currentNode.Children[indexOfNewKey + 1];
        }

        private void BreakUpNode(Node<T> descendant, int indexOfNodeInAncestor = -1,
            Node<T> ancestor = null, bool descendantIsRoot = true)
        {
            var keyInParent = descendant.Keys[DegreeOfTree - 1];
            var (newLeftDescendant, newRightDescendant) = SplitDescendant(descendant);
            if (descendantIsRoot)
                LinkNewDescendantsToNewRoot(keyInParent, newLeftDescendant, newRightDescendant);
            else
                LinkNewDescendantsToAncestor(indexOfNodeInAncestor, ancestor,
                    keyInParent, newLeftDescendant, newRightDescendant);
        }

        private void LinkNewDescendantsToAncestor(int indexOfNodeInAncestor,
            Node<T> ancestor, T keyInParent, Node<T> leftNode, Node<T> rightNode)
        {
            ancestor.Keys.Insert(indexOfNodeInAncestor, keyInParent);
            ancestor.Children[indexOfNodeInAncestor] = leftNode;
            ancestor.Children.Insert(indexOfNodeInAncestor + 1, rightNode);
        }

        private void LinkNewDescendantsToNewRoot(T keyInNewRoot,
            Node<T> leftNode, Node<T> rightNode)
        {
            root = new Node<T>(false, Enumerable.Repeat(keyInNewRoot, 1));
            root.Children.Add(leftNode);
            root.Children.Add(rightNode);
            TreeHeight++;
        }

        private (Node<T> leftNode, Node<T> rightNode) SplitDescendant(Node<T> oldDescendant)
        {
            var leftNode = new Node<T>(oldDescendant.IsLeaf, oldDescendant.Keys.Take(DegreeOfTree - 1));
            var rightNode = new Node<T>(oldDescendant.IsLeaf, oldDescendant.Keys.TakeLast(DegreeOfTree - 1));
            if (!oldDescendant.IsLeaf)
                AddChildrenToNewDescendants(leftNode, rightNode, oldDescendant);
            return (leftNode, rightNode);
        }

        private void AddChildrenToNewDescendants(Node<T> leftNode, Node<T> rightNode, Node<T> child)
        {
            leftNode.Children.AddRange(child.Children.Take(DegreeOfTree));
            rightNode.Children.AddRange(child.Children.TakeLast(DegreeOfTree));
        }

        public bool Find(T item)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                var nextIndex = 0;
                for (; nextIndex < currentNode.Keys.Count; nextIndex++)
                {
                    if (item.CompareTo(currentNode.Keys[nextIndex]) == 0)
                        return true;
                    if (item.CompareTo(currentNode.Keys[nextIndex]) < 0)
                        break;
                }
                if (currentNode.IsLeaf)
                    return false;
                currentNode = currentNode.Children[nextIndex];
            }
            return false;
        }

        public bool Remove(T item)
        {
            Node<T> ancestor = null;
            Node<T> nodeWihRemovingKey = null;
            if (!TryToFindNodeWithItemAndAncestorForHim(item, ref nodeWihRemovingKey, ref ancestor))
                return false;
            (nodeWihRemovingKey, ancestor) = TransferRemovingKeyToTheLeaf(item, nodeWihRemovingKey, ancestor);
            if (nodeWihRemovingKey == root && nodeWihRemovingKey.Keys.Count == 1)
            {
                root = null;
                Count--;
                TreeHeight--;
                return true;
            }
            if (nodeWihRemovingKey.Keys.Count == DegreeOfTree - 1 && nodeWihRemovingKey != root)
                nodeWihRemovingKey = RestoreBalanceInNode(nodeWihRemovingKey, ancestor);
            nodeWihRemovingKey.Keys.Remove(item);
            Count--;
            return true;
        }

        private bool TryToFindNodeWithItemAndAncestorForHim(T item, ref Node<T> nodeWithItem,
            ref Node<T> ancestor)
        {
            if (root == null)
                return false;
            nodeWithItem = root;
            int numberDescendant;
            while (!nodeWithItem.Keys.Contains(item))
            {
                if (nodeWithItem.IsLeaf)
                    return false;
                numberDescendant = nodeWithItem.Keys.FindLastIndex(key => item.CompareTo(key) > 0) + 1;
                ancestor = nodeWithItem;
                nodeWithItem = nodeWithItem.Children[numberDescendant];
                if (nodeWithItem.Keys.Count == DegreeOfTree - 1)
                    nodeWithItem = RestoreBalanceInNode(nodeWithItem, ancestor);
            }
            return true;
        }

        private (Node<T> leafWithRemovingKey, Node<T> ancestorForLeaf)
            TransferRemovingKeyToTheLeaf(T item, Node<T> nodeWithRemovingKey, Node<T> ancestorForLeaf)
        {
            int indexOfRemovingKey;
            while (!nodeWithRemovingKey.IsLeaf)
            {
                ancestorForLeaf = nodeWithRemovingKey;
                indexOfRemovingKey = nodeWithRemovingKey.Keys.FindLastIndex(key => item.CompareTo(key) > 0) + 1;
                nodeWithRemovingKey = MoveKeyOneLevelDown(nodeWithRemovingKey, indexOfRemovingKey);
            }
            return (nodeWithRemovingKey, ancestorForLeaf);
        }

        private Node<T> RestoreBalanceInNode(Node<T> node, Node<T> parentForNode)
        {
            var numberOfLeafInParent = parentForNode.Children.IndexOf(node);
            if (numberOfLeafInParent > 0 &&
                parentForNode.Children[numberOfLeafInParent - 1].Keys.Count > DegreeOfTree - 1)
                InsertOneKeyFromLeftNode(parentForNode, numberOfLeafInParent - 1);
            else if (numberOfLeafInParent < parentForNode.Children.Count - 1 &&
                parentForNode.Children[numberOfLeafInParent + 1].Keys.Count > DegreeOfTree - 1)
                InsertOneKeyFromRightNode(parentForNode, numberOfLeafInParent + 1);
            else if (numberOfLeafInParent > 0)
                return Merge(parentForNode, numberOfLeafInParent - 1);
            else
                return Merge(parentForNode, numberOfLeafInParent);
            return node;
        }


        private void InsertOneKeyFromLeftNode(Node<T> parentForCurrentNode,
            int numberOfLeftNodeInParent)
        {
            MoveKeyOnRight(parentForCurrentNode, numberOfLeftNodeInParent);
        }

        private void InsertOneKeyFromRightNode(Node<T> parentForCurrentNode,
            int numberOfRightNodeInParent)
        {
            MoveKeyOnLeft(parentForCurrentNode, numberOfRightNodeInParent - 1);
        }

        private Node<T> MoveKeyOneLevelDown(Node<T> nodeHavingKey, int indexOfKey)
        {
            if (nodeHavingKey.Children[indexOfKey].Keys.Count > DegreeOfTree - 1)
                return MoveKeyOnRight(nodeHavingKey, indexOfKey);
            else if (nodeHavingKey.Children[indexOfKey + 1].Keys.Count > DegreeOfTree - 1)
                return MoveKeyOnLeft(nodeHavingKey, indexOfKey);
            else
                return Merge(nodeHavingKey, indexOfKey);
        }

        private Node<T> MoveKeyOnLeft(Node<T> nodeHavingKey, int indexOfKey)
        {
            var key = nodeHavingKey.Keys[indexOfKey];
            var removedItem = nodeHavingKey.Children[indexOfKey + 1].Keys[0];
            nodeHavingKey.Children[indexOfKey + 1].Keys.RemoveAt(0);
            nodeHavingKey.Keys[indexOfKey] = removedItem;
            nodeHavingKey.Children[indexOfKey].Keys.Add(key);
            if (!nodeHavingKey.Children[indexOfKey].IsLeaf)
            {
                nodeHavingKey.Children[indexOfKey].Children
                    .Add(nodeHavingKey.Children[indexOfKey + 1].Children[0]);
                nodeHavingKey.Children[indexOfKey + 1].Children.RemoveAt(0);
            }
            return nodeHavingKey.Children[indexOfKey];
        }

        private Node<T> MoveKeyOnRight(Node<T> nodeHavingKey, int indexOfKey)
        {
            var key = nodeHavingKey.Keys[indexOfKey];
            var lastIndexRemovedItem = nodeHavingKey.Children[indexOfKey].Keys.Count - 1;
            var removedItem = nodeHavingKey.Children[indexOfKey].Keys[lastIndexRemovedItem];
            nodeHavingKey.Children[indexOfKey].Keys.RemoveAt(lastIndexRemovedItem);
            nodeHavingKey.Keys[indexOfKey] = removedItem;
            nodeHavingKey.Children[indexOfKey + 1].Keys.Insert(0, key);
            if (!nodeHavingKey.Children[indexOfKey].IsLeaf)
            {
                nodeHavingKey.Children[indexOfKey + 1].Children
                    .Insert(0, nodeHavingKey.Children[indexOfKey].Children[lastIndexRemovedItem + 1]);
                nodeHavingKey.Children[indexOfKey].Children.RemoveAt(lastIndexRemovedItem + 1);
            }
            return nodeHavingKey.Children[indexOfKey + 1];
        }

        private Node<T> Merge(Node<T> nodeHavingKey, int indexOfKey)
        {
            var (leftNode, rightNode) = (nodeHavingKey.Children[indexOfKey], nodeHavingKey.Children[indexOfKey + 1]);
            Node<T> newNode = CreateNewNodeWithCopiedKeys(nodeHavingKey, indexOfKey, leftNode, rightNode);
            if (!newNode.IsLeaf)
                newNode.Children.AddRange(leftNode.Children.Concat(rightNode.Children));
            LinkNewNodeWithParent(nodeHavingKey, indexOfKey, newNode);
            if (nodeHavingKey == root && root.Keys.Count == 0)
            {
                root = newNode;
                TreeHeight--;
            }
            return newNode;
        }

        private void LinkNewNodeWithParent(Node<T> parent, int indexOfKey, Node<T> newNode)
        {
            parent.Children[indexOfKey] = newNode;
            parent.Children.RemoveAt(indexOfKey + 1);
            parent.Keys.RemoveAt(indexOfKey);
        }

        private Node<T> CreateNewNodeWithCopiedKeys(Node<T> nodeHavingKey, int indexOfKey,
            Node<T> leftNode, Node<T> rightNode)
        {
            var newNode = new Node<T>(leftNode.IsLeaf, leftNode.Keys);
            newNode.Keys.Add(nodeHavingKey.Keys[indexOfKey]);
            newNode.Keys.AddRange(rightNode.Keys);
            return newNode;
        }
    }
}
