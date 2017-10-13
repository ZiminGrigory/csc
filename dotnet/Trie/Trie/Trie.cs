using System.Collections.Generic;

namespace Trie
{
    public class Trie : ITrie
    {
        public Trie()
        {
             _root = new Node();
        }

        public bool Add(string element)
        {
            if (Contains(element))
            {
                return false;
            }

            TraverseAndCreate(_root, element, 0);
            return true;
        }

        public bool Contains(string element)
        {
            Node tmp = _root;
            foreach (var ch in element)
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
            {
                return false;
            }

            Node tmp = _root;
            --tmp.Size;
            foreach (var ch in element)
            {
                if (tmp.Size == 0)
                {
                    tmp.Children.Remove(ch);
                    return true;
                }

                tmp = tmp.Children[ch];
                --tmp.Size;
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
            foreach(var ch in element)
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
                ++root.Size;
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
                ++pathPosition;
            }
        }

        private class Node
        {
            public Node()
            {
                Children = new Dictionary<char, Node>();
            }

            public bool IsTerminal { get; set; }
            public int Size { get; set; }
            public readonly Dictionary<char, Node> Children;
        }

        private readonly Node _root;
    }
}