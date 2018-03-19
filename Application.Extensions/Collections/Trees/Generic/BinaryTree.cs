using System;
using System.Collections.Generic;

namespace Application.Extensions.Collections.Trees.Generic
{
    public class BinaryTree<T> : IEnumerable<T> where T : IComparable<T>
    {
        private BinaryTreeNode<T> _head;
        private int _count;

        public void Add(T value)
        {
            // Case 1: The tree is empty. Allocate the head. 
            if (_head == null)
            {
                _head = new BinaryTreeNode<T>(value);
            }
            // Case 2: The tree is not empty, so recursively 
            // find the right location to insert the node. 
            else
            {
                AddTo(_head, value);
            }
            _count++;
        }

        // Recursive add algorithm. 
        private void AddTo(BinaryTreeNode<T> node, T value)
        {
            // Case 1: Value is less than the current node value 
            if (value.CompareTo(node.Value) < 0)
            {
                // If there is no left child, make this the new left, 
                if (node.Left == null)
                {
                    node.Left = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // else add it to the left node. 
                    AddTo(node.Left, value);
                }
            }
            // Case 2: Value is equal to or greater than the current value.
            else
            {
                // If there is no right, add it to the right, 
                if (node.Right == null)
                {
                    node.Right = new BinaryTreeNode<T>(value);
                }
                else
                {
                    // else add it to the right node. 
                    AddTo(node.Right, value);
                }
            }
        }

        public bool Contains(T value)
        {
            // Defer to the node search helper function. 
            BinaryTreeNode<T> parent;
            return FindWithParent(value, out parent) != null;
        }

        private BinaryTreeNode<T> FindWithParent(T value, out BinaryTreeNode<T> parent)
        {
            // Now, try to find data in the tree. 
            BinaryTreeNode<T> current = _head;
            parent = null;
            // While we don't have a match...
            while (current != null)
            {
                int result = current.CompareTo(value);
                if (result > 0)
                {
                    // If the value is less than current, go left. 
                    parent = current;
                    current = current.Left;
                }
                else if (result < 0)
                {
                    // If the value is more than current, go right. 
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    // We have a match! 
                    break;
                }
            }
            return current;
        }

        public bool Remove(T value)
        {
            BinaryTreeNode<T> current, parent;

            // Find the node to remove. 
            current = FindWithParent(value, out parent);
            if (current == null)
            {
                return false;
            }
            _count--;
            // Case 1: If current has no right child, current's left replaces current. 
            if (current.Right == null)
            {
                if (parent == null)
                {
                    _head = current.Left;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value, 
                        // make the current left child a left child of parent. 
                        parent.Left = current.Left;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value, 
                        // make the current left child a right child of parent. 
                        parent.Right = current.Left;
                    }
                }
            }
            // Case 2: If current's right child has no left child, current's right child 
            // replaces current.
            else if (current.Right.Left == null)
            {
                current.Right.Left = current.Left;
                if (parent == null)
                {
                    _head = current.Right;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value, 
                        // make the current right child a left child of parent.
                        parent.Left = current.Right;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value, 
                        // make the current right child a right child of parent. 
                        parent.Right = current.Right;
                    }

                }
            }
            // Case 3: If current's right child has a left child, replace current with current's
            // right child's left-most child.
            else
            {
                // Find the right's left-most child. 
                BinaryTreeNode<T> leftmost = current.Right.Left;
                BinaryTreeNode<T> leftmostParent = current.Right;
                while (leftmost.Left != null)
                {
                    leftmostParent = leftmost;
                    leftmost = leftmost.Left;
                }
                // The parent's left subtree becomes the leftmost's right subtree. 
                leftmostParent.Left = leftmost.Right;
                // Assign leftmost's left and right to current's left and right children. 
                leftmost.Left = current.Left;
                leftmost.Right = current.Right;
                if (parent == null)
                {
                    _head = leftmost;
                }
                else
                {
                    int result = parent.CompareTo(current.Value);
                    if (result > 0)
                    {
                        // If parent value is greater than current value, 
                        // make leftmost the parent's left child. 
                        parent.Left = leftmost;
                    }
                    else if (result < 0)
                    {
                        // If parent value is less than current value, 
                        // make leftmost the parent's right child. 
                        parent.Right = leftmost;
                    }
                }
            }
            return true;
        }

        public void PreOrderTraversal(Action<T> action) { PreOrderTraversal(action, _head); }
        private void PreOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                action(node.Value);
                PreOrderTraversal(action, node.Left);
                PreOrderTraversal(action, node.Right);
            }
        }

        public void PostOrderTraversal(Action<T> action) { PostOrderTraversal(action, _head); }
        private void PostOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(action, node.Left);
                PostOrderTraversal(action, node.Right);
                action(node.Value);
            }
        }

        public void InOrderTraversal(Action<T> action) { InOrderTraversal(action, _head); }
        private void InOrderTraversal(Action<T> action, BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(action, node.Left);
                action(node.Value);
                InOrderTraversal(action, node.Right);
            }
        }
        public IEnumerator<T> InOrderTraversal()
        {
            // This is a non-recursive algorithm using a stack to demonstrate removing 
            // recursion. 
            if (_head != null)
            {
                // Store the nodes we've skipped in this stack (avoids recursion). 
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> current = _head;
                // When removing recursion, we need to keep track of whether 
                // we should be going to the left node or the right nodes next. 
                bool goLeftNext = true;
                // Start by pushing Head onto the stack. 
                stack.Push(current);
                while (stack.Count > 0)
                {
                    // If we're heading left... 
                    if (goLeftNext)
                    {
                        // Push everything but the left-most node to the stack. 
                        // We'll yield the left-most after this block. 
                        while (current.Left != null)
                        { stack.Push(current); current = current.Left; }
                    }
                    // Inorder is left->yield->right. 
                    yield return current.Value;
                    // If we can go right, do so. 
                    if (current.Right != null)
                    {
                        current = current.Right;
                        // Once we've gone right once, we need to start 
                        // going left again. 
                        goLeftNext = true;
                    }
                    else
                    {
                        // If we can't go right, then we need to pop off the parent node 
                        // so we can process it and then go to its right node. 
                        current = stack.Pop();
                        goLeftNext = false;
                    }
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InOrderTraversal();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public void Clear() { _head = null; _count = 0; }

        public int Count { get { return _count; } }

        private T ElementAt(BinaryTreeNode<T> node)
        {
            return node == null ? default(T) : node.Value;
        }

        public T Root()
        {
            return ElementAt(Root(this._head));
        }

        private BinaryTreeNode<T> Root(BinaryTreeNode<T> node)
        {
            return node;
        }

        public T FindMax()
        {
            return ElementAt(FindMax(this._head));
        }

        private BinaryTreeNode<T> FindMax(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                while (node.Right != null)
                {
                    node = node.Right;
                }
            }
            return node;
        }

        public T FindMin()
        {
            return ElementAt(FindMin(this._head));
        }

        private BinaryTreeNode<T> FindMin(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                while (node.Left != null)
                {
                    node = node.Left;
                }
            }
            return node;
        }
    }
}
