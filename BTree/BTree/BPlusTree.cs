using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTree
{
    public class BPlusTree<T>: ITree<T>
        where T : IComparable
    {
        class Node<TNode>
        {
            public bool IsLeaf { get; }
            public List<TNode> Keys { get; set; }
            public List<Node<TNode>> Children { get; set; }
            public Node<TNode> Parent { get; set; }
            public Node<TNode> Left { get; set; }
            public Node<TNode> Right { get; set; }

            public Node(bool isLeaf, IEnumerable<TNode> keys = null, Node<TNode> parent = null,
                Node<TNode> left = null, Node<TNode> right = null)
            {
                IsLeaf = isLeaf;
                Keys = keys == null ? new List<TNode>() : new List<TNode>(keys);
                Parent = parent;
                Left = left;
                Right = right;
                if (!isLeaf)
                    Children = new List<Node<TNode>>();
            }
        }

        private Node<T> root;
        public int DegreeOfTree { get; }
        private readonly int maxAmountOfKeys;

        public BPlusTree(int degreeOfTree, IEnumerable<T> source = null)
        {
            DegreeOfTree = degreeOfTree;
            maxAmountOfKeys = 2 * degreeOfTree;
            if (source != null)
                foreach (var key in source)
                    this.Add(key);
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (root == null)
                yield break;
            var currentNode = root;
            while (!currentNode.IsLeaf)
                currentNode = currentNode.Children[0];
            while(currentNode != null)
            {
                foreach (var key in currentNode.Keys)
                    yield return key;
                currentNode = currentNode.Right;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Find(T item)
        {
            if (root == null)
                return false;
            var leaf = FindLeaf(item);
            return leaf.Keys.Contains(item);
        }

        private Node<T> FindLeaf(T item)
        {
            var node = root;
            int numberOfChild = 0;
            while (!node.IsLeaf)
            {
                for (numberOfChild = 0; numberOfChild < node.Keys.Count; numberOfChild++)
                    if (node.Keys[numberOfChild].CompareTo(item) > 0)
                        break;
                node = node.Children[numberOfChild];
            }
            return node;
        }

        public void Add(T item)
        {
            if (root == null)
                root = new Node<T>(true);
            var leaf = FindLeaf(item);
            int indexOfInsert = leaf.Keys.FindFirstIndexOrCountOfList(item);
            leaf.Keys.Insert(indexOfInsert, item);
            if (leaf.Keys.Count == maxAmountOfKeys)
                SplitNode(leaf);
        }

        private void SplitNode(Node<T> node)
        {
            var (leftNode, rightNode) = CreateLeftAndRightNode(node);
            if (!node.IsLeaf)
                ConnectNewNodesWithChildren(node, leftNode, rightNode);
            if (node == root)
                node = ConnectNewNodesWithNewRoot(leftNode, rightNode);
            else
            {
                ConnectNewNodesWithNeighbors(node, leftNode, rightNode);
                ConnectNewNodesWithParent(node, leftNode, rightNode);
            }
            if (node.Parent?.Keys.Count == maxAmountOfKeys)
                SplitNode(node.Parent);
        }

        private void ConnectNewNodesWithParent(Node<T> node, Node<T> leftNode, Node<T> rightNode)
        {
            var numberOfChild = node.Parent.Children.IndexOf(node);
            node.Parent.Children[numberOfChild] = leftNode;
            node.Parent.Children.Insert(numberOfChild + 1, rightNode);
            node.Parent.Keys.Insert(numberOfChild, node.Keys[DegreeOfTree]);
        }

        private Node<T> ConnectNewNodesWithNewRoot(Node<T> leftNode, Node<T> rightNode)
        {
            Node<T> newRoot = new Node<T>(false);
            newRoot.Keys.Add(root.Keys[DegreeOfTree]);
            newRoot.Children.Add(leftNode);
            newRoot.Children.Add(rightNode);
            (leftNode.Parent, rightNode.Parent) = (newRoot, newRoot);
            root = newRoot;
            return newRoot;
        }

        private static void ConnectNewNodesWithNeighbors(Node<T> node, Node<T> leftNode, Node<T> rightNode)
        {
            if (node.Left != null)
                node.Left.Right = leftNode;
            if (node.Right != null)
                node.Right.Left = rightNode;
        }

        private void ConnectNewNodesWithChildren(Node<T> node, Node<T> leftNode, Node<T> rightNode)
        {
            leftNode.Children.AddRange(node.Children.Take(DegreeOfTree + 1));
            rightNode.Children.AddRange(node.Children.TakeLast(DegreeOfTree));
            foreach (var child in leftNode.Children)
                child.Parent = leftNode;
            foreach (var child in rightNode.Children)
                child.Parent = rightNode;
        }

        private (Node<T> leftNode, Node<T> rightNode) CreateLeftAndRightNode(Node<T> node)
        {
            var leftNode = new Node<T>(node.IsLeaf, node.Keys.Take(DegreeOfTree), node.Parent, node.Left);
            var rightNode = new Node<T>(node.IsLeaf, node.IsLeaf ? node.Keys.TakeLast(DegreeOfTree) : 
                node.Keys.TakeLast(DegreeOfTree - 1), node.Parent, leftNode, node.Right);
            leftNode.Right = rightNode;
            return (leftNode, rightNode);
        }

        public bool Remove(T item)
        {
            if (root == null)
                return false;
            var leafWithItem = FindLeaf(item);
            if (!leafWithItem.Keys.Contains(item))
                return false;
            if (leafWithItem.Keys[0].CompareTo(item) == 0 && leafWithItem.Left != null)
                ChangeKeyInTree(leafWithItem, item, leafWithItem.Keys[1]);
            leafWithItem.Keys.Remove(item);
            if (leafWithItem.Keys.Count == 0 && leafWithItem == root)
                root = null;
            else if (leafWithItem.Keys.Count < DegreeOfTree - 1 && leafWithItem != root)
                RestoreBalanceInNode(leafWithItem);
            return true;
        }

        private void ChangeKeyInTree(Node<T> leafWithKeyToDelete, T keyToDelete, T newKey)
        {

            while (!leafWithKeyToDelete.Parent.Keys.Contains(keyToDelete))
                leafWithKeyToDelete = leafWithKeyToDelete.Parent;
            var indexOldKey = leafWithKeyToDelete.Parent.Children.IndexOf(leafWithKeyToDelete) - 1;
            leafWithKeyToDelete.Parent.Keys[indexOldKey] = newKey;
        }

        private void RestoreBalanceInNode(Node<T> node)
        {
            if (node.Right?.Keys.Count > DegreeOfTree - 1 && node.Right?.Parent == node.Parent)
                TakeKeyFromRightNode(node);
            else if (node.Left?.Keys.Count > DegreeOfTree - 1 && node.Left?.Parent == node.Parent)
                TakeKeyFromLeftNode(node);
            else if (node.Left != null && node.Left?.Parent == node.Parent)
                node = Merge(node.Left, node);
            else
                node = Merge(node, node.Right);
            if (node.Parent != root && node.Parent?.Keys.Count < DegreeOfTree - 1)
                RestoreBalanceInNode(node.Parent);
        }

        private void TakeKeyFromRightNode(Node<T> node)
        {
            if (node.IsLeaf)
                TakeKeyFromRightLeaf(node);
            else
                TakeKeyFromRightInternalNode(node);
        }

        private void TakeKeyFromLeftNode(Node<T> node)
        {
            if (node.IsLeaf)
                TakeKeyFromLeftLeaf(node);
            else
                TakeKeyFromLeftInternalNode(node);
        }

        private void TakeKeyFromLeftLeaf(Node<T> node)
        {
            var indexLastKeyInLeftNode = node.Left.Keys.Count - 1;
            var addedKey = node.Left.Keys[indexLastKeyInLeftNode];
            node.Left.Keys.RemoveAt(indexLastKeyInLeftNode);
            node.Keys.Insert(0, addedKey);
            var indexKeyInParent = node.Parent.Children.IndexOf(node) - 1;
            node.Parent.Keys[indexKeyInParent] = addedKey;
        }

        private void TakeKeyFromRightLeaf(Node<T> node)
        {
            node.Keys.Add(node.Right.Keys[0]);
            node.Right.Keys.RemoveAt(0);
            var indexKeyInParent = node.Parent.Children.IndexOf(node);
            node.Parent.Keys[indexKeyInParent] = node.Right.Keys[0];
        }

        private void TakeKeyFromRightInternalNode(Node<T> node)
        {
            var keyFromRightNeighbour = node.Right.Keys[0];
            node.Right.Keys.RemoveAt(0);
            var indexOfKeyFromParent = node.Parent.Children.IndexOf(node);
            node.Keys.Add(node.Parent.Keys[indexOfKeyFromParent]);
            node.Parent.Keys[indexOfKeyFromParent] = keyFromRightNeighbour;

            var child = node.Right.Children[0];
            node.Right.Children.RemoveAt(0);
            node.Children.Add(child);
            child.Parent = node;
        }

        private void TakeKeyFromLeftInternalNode(Node<T> node)
        {
            var keyFromLeftNeighbour = node.Left.Keys[node.Left.Keys.Count - 1];
            var indexOfKeyFromParent = node.Parent.Children.IndexOf(node) - 1;
            node.Keys.Insert(0, node.Parent.Keys[indexOfKeyFromParent]);
            node.Parent.Keys[indexOfKeyFromParent] = keyFromLeftNeighbour;
            node.Left.Keys.RemoveAt(node.Left.Keys.Count - 1);

            var child = node.Left.Children[node.Left.Children.Count - 1];
            node.Left.Children.RemoveAt(node.Left.Children.Count - 1);
            node.Children.Insert(0, child);
            child.Parent = node;
        }

        private Node<T> Merge(Node<T> leftNode, Node<T> rightNode)
        {
            AddKeysInLeftNode(leftNode, rightNode);
            if (!leftNode.IsLeaf)
                AddChildrenInLeftNodeFromRightNode(leftNode, rightNode);
            rightNode.Parent.Children.Remove(rightNode);
            LinkLeftNodeWithNewRightNode(leftNode, rightNode);
            if (leftNode.Parent.Keys.Count == 0)
            {
                root = leftNode;
                leftNode.Parent = null;
            }
            return leftNode;
        }

        private static void LinkLeftNodeWithNewRightNode(Node<T> leftNode, Node<T> rightNode)
        {
            leftNode.Right = rightNode.Right;
            if (rightNode.Right != null)
                rightNode.Right.Left = leftNode;
        }

        private void AddKeysInLeftNode(Node<T> leftNode, Node<T> rightNode)
        {
            var indexOfKey = leftNode.Parent.Children.IndexOf(leftNode);
            if (!leftNode.IsLeaf)
                leftNode.Keys.Add(leftNode.Parent.Keys[indexOfKey]);
            leftNode.Keys.AddRange(rightNode.Keys);
            rightNode.Parent.Keys.RemoveAt(indexOfKey);
        }

        private void AddChildrenInLeftNodeFromRightNode(Node<T> leftLeaf, Node<T> rightLeaf)
        {
            leftLeaf.Children.AddRange(rightLeaf.Children);
            foreach (var child in rightLeaf.Children)
                child.Parent = leftLeaf;
        }
    }
}
