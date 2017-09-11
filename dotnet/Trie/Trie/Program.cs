using System;
using System.Collections.Generic;

namespace Trie
{
    internal interface ITrie
    {    /// Expected complexity: O(|element|)
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
                if (tmp.childs.ContainsKey(ch))
                {
                    tmp = tmp.childs[ch];
                }
                else
                {
                    return false;
                }
            }

            return tmp.isTerminal;
        }

        public bool Remove(string element)
        {
            if (!Contains(element))
                return false;

            Node tmp = _root;
            tmp.size--;
            foreach(char ch in element)
            {
                if (tmp.size == 0)
                {
                    tmp.childs.Remove(ch);
                    return true;
                }

                tmp = tmp.childs[ch];
                tmp.size--;
            }

            tmp.isTerminal = false;
            return true;
        }

        public int Size()
        {
            return _root.size;
        }

        public int HowManyStartsWithPrefix(string element)
        {
            Node tmp = _root;
            foreach(char ch in element)
            {
                if (tmp.childs.ContainsKey(ch))
                {
                    tmp = tmp.childs[ch];
                }
                else
                {
                    return 0;
                }
            }

            return tmp.size;
        }

        private void TraverseAndCreate(Node root, string path, int pathPosition)
        {
            while (true)
            {
                root.size++;
                if (pathPosition == path.Length)
                {
                    root.isTerminal = true;
                    return;
                }

                char next = path[pathPosition];
                if (root.childs.ContainsKey(next))
                {
                    TraverseAndCreate(root.childs[next], path, ++pathPosition);
                    return;
                }

                Node newNode = new Node();
                root.childs.Add(next, newNode);
                root = newNode;
                pathPosition++;
            }
        }

        private class Node
        {
            public Node()
            {
                childs = new Dictionary<char, Node>();
            }

            public bool isTerminal;
            public readonly Dictionary<char, Node> childs;
            public int size;
        }

        private readonly Node _root;
    }

    internal class Program
    {
        public static void TrieConsoleTest()
        {
            ITrie trie = new Trie();
            trie.Add("a");
            trie.Add("aa");
            trie.Add("aaa");
            trie.Add("aab");
            trie.Add("aac");

            Console.WriteLine("Trie size after 5 unique string were added: {0}", trie.Size());
            Console.WriteLine("HowManyStartsWithPrefix \"a\" (should be 5): {0}", trie.HowManyStartsWithPrefix("a"));
            Console.WriteLine("HowManyStartsWithPrefix \"aa\" (should be 4): {0}", trie.HowManyStartsWithPrefix("aa"));
            Console.WriteLine("HowManyStartsWithPrefix \"aac\" (should be 1): {0}", trie.HowManyStartsWithPrefix("aac"));
            Console.WriteLine("HowManyStartsWithPrefix \"s\" (should be 0): {0}\n", trie.HowManyStartsWithPrefix("s"));

            trie.Remove("a");
            trie.Remove("a");
            Console.WriteLine("Trie size after \"a\" was removed (should be 4): {0}", trie.Size());
            Console.WriteLine("HowManyStartsWithPrefix \"a\" (should be 4): {0}", trie.HowManyStartsWithPrefix("a"));
            Console.WriteLine("HowManyStartsWithPrefix \"aa\" (should be 4): {0}", trie.HowManyStartsWithPrefix("aa"));
            Console.WriteLine("HowManyStartsWithPrefix \"aac\" (should be 1): {0}", trie.HowManyStartsWithPrefix("aac"));
            Console.WriteLine("HowManyStartsWithPrefix \"s\" (should be 0): {0}", trie.HowManyStartsWithPrefix("s"));
            Console.WriteLine("Contains \"aac\" (should be True): {0}", trie.Contains("aac"));
            Console.WriteLine("remove \"aac\"");
            trie.Remove("aac");
            Console.WriteLine("Contains \"aac\" (should be False): {0}\n", trie.Contains("aac"));

            Console.WriteLine("Trie size (should be 3): {0}", trie.Size());
            Console.WriteLine("Contains \"a\" (should be False): {0}", trie.Contains("a"));
            Console.WriteLine("Contains \"aa\" (should be True): {0}", trie.Contains("aa"));
            Console.WriteLine("Contains \"aaa\" (should be True): {0}", trie.Contains("aaa"));
            Console.WriteLine("Contains \"aab\" (should be True): {0}", trie.Contains("aab"));
            Console.WriteLine("Contains \"aac\" (should be False): {0}", trie.Contains("aac"));
        }

        public static void TrieMassAddRemoveTest()
        {
            ITrie trie = new Trie();
            String s1 = "asdasdyu2ge12bd1ui2re98fjauifbsdjkfnn21342342312asdfasfas";
            String s2 = "zasfqr32jrb23hjrb2j3r23r32r23r23r32r";
            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Add(s1.Substring(0, i));
                if (trie.Size() != i)
                {
                    Console.WriteLine("Fail_1");
                }
            }

            int curSize = trie.Size();

            for (var i = 1; i <= s2.Length; ++i)
            {
                trie.Add(s2.Substring(0, i));
                if (trie.Size() != curSize + i)
                {
                    Console.WriteLine("Fail_2");
                }
            }

            curSize = trie.Size();

            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Add(s1.Substring(0, i));
                if (trie.Size() != curSize)
                {
                    Console.WriteLine("Fail_3");
                }
            }

            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Remove(s1.Substring(0, i));
            }

            for (var i = 1; i <= s2.Length; ++i)
            {
                trie.Remove(s2.Substring(0, i));
            }

            if (trie.Size() != 0)
            {
                Console.WriteLine("Fail_4");
            }
        }

        public static void Main(string[] args)
        {
            TrieConsoleTest();
            TrieMassAddRemoveTest();
        }
    }
}