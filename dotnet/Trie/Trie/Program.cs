using System;
using System.Collections;

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
            return false;
        }

        public bool Contains(string element)
        {
            return false;
        }

        public bool Remove(string element)
        {
            return false;
        }

        public int Size()
        {
            return 0;
        }

        public int HowManyStartsWithPrefix(string element)
        {
            return 0;
        }

        private class Node
        {
            public char character;
            public bool elementLast;
            public Hashtable childs;
        }

        private Node _root;
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}