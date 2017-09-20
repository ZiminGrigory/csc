using System;
using System.Collections.Generic;

namespace Trie
{
    public interface ITrie
    {    
        /// Expected complexity: O(|element|)
        /// Returns true if this set did not already contain the specified element
        bool Add(string element);

        /// Expected complexity: O(|element|)
        bool Contains(string element);

        /// Expected complexity: O(|element|)
        /// Returns true if this set contained the specified element
        bool Remove(string element);

        /// Expected complexity: O(1)
        int Size();

        /// Expected complexity: O(|prefix|)
        int HowManyStartsWithPrefix(string prefix);
    }

    public class Trie : ITrie
    {
        public Trie()
        {
             _root = new Node();
        }

        public bool Add(string element)
        {
            if (Contains(element))
                return false;

            TraverseAndCreate(_root, element, 0);
            return true;
        }

        public bool Contains(string element)
        {
            Node tmp = _root;
            foreach(char ch in element)
            {
                if (tmp.Children.ContainsKey(ch))
                {
                    tmp = tmp.Children[ch];
                }
                else
                {
                    return false;
                }
            }

            return tmp.IsTerminal;
        }

        public bool Remove(string element)
        {
            if (!Contains(element))
                return false;

            Node tmp = _root;
            tmp.Size--;
            foreach(char ch in element)
            {
                if (tmp.Size == 0)
                {
                    tmp.Children.Remove(ch);
                    return true;
                }

                tmp = tmp.Children[ch];
                tmp.Size--;
            }

            tmp.IsTerminal = false;
            return true;
        }

        public int Size()
        {
            return _root.Size;
        }

        public int HowManyStartsWithPrefix(string element)
        {
            Node tmp = _root;
            foreach(char ch in element)
            {
                if (tmp.Children.ContainsKey(ch))
                {
                    tmp = tmp.Children[ch];
                }
                else
                {
                    return 0;
                }
            }

            return tmp.Size;
        }

        private static void TraverseAndCreate(Node root, string path, int pathPosition)
        {
            while (true)
            {
                root.Size++;
                if (pathPosition == path.Length)
                {
                    root.IsTerminal = true;
                    return;
                }

                char next = path[pathPosition];
                if (root.Children.ContainsKey(next))
                {
                    TraverseAndCreate(root.Children[next], path, ++pathPosition);
                    return;
                }

                var newNode = new Node();
                root.Children.Add(next, newNode);
                root = newNode;
                pathPosition++;
            }
        }

        private class Node
        {
            public Node()
            {
                Children = new Dictionary<char, Node>();
            }

            public bool IsTerminal { get; set; }
            public Dictionary<char, Node> Children { get; }
            public int Size { get; set; }
        }

        private readonly Node _root;
    }
}