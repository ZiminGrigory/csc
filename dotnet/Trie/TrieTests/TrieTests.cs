using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trie;

namespace TrieTests
{
    [TestClass()]
    public class TrieTests
    {
        [TestMethod()]
        public void TrieTest()
        {
            ITrie trie = new Trie.Trie();
            Assert.IsNotNull(trie);
        }

        [TestMethod()]
        public void AddTest()
        {
            ITrie trie = new Trie.Trie();
            Assert.IsTrue(trie.Add("a"));
            Assert.IsTrue(trie.Add("aa"));
            Assert.IsTrue(trie.Add("aaa"));
            Assert.IsTrue(trie.Add("aab"));
            Assert.IsTrue(trie.Add("aac"));
        }

        [TestMethod()]
        public void ContainsTest()
        {
            ITrie trie = new Trie.Trie();
            trie.Add("a");
            trie.Add("aa");
            trie.Add("aaa");
            trie.Add("aab");
            trie.Add("aac");
            Assert.IsFalse(trie.Contains(""));
            Assert.IsTrue(trie.Contains("a"));
            Assert.IsTrue(trie.Contains("aa"));
            Assert.IsTrue(trie.Contains("aaa"));
            Assert.IsTrue(trie.Contains("aab"));
            Assert.IsTrue(trie.Contains("aac"));
        }

        [TestMethod()]
        public void RemoveTest()
        {
            ITrie trie = new Trie.Trie();
            trie.Add("a");
            trie.Add("aa");
            trie.Add("aaa");
            trie.Add("aab");
            trie.Add("aac");
            Assert.IsTrue(trie.Remove("a"));
            Assert.IsFalse(trie.Remove("a"));
            Assert.IsFalse(trie.Remove(""));
        }

        [TestMethod()]
        public void SizeTest()
        {
            ITrie trie = new Trie.Trie();
            Assert.AreEqual(0, trie.Size());
            trie.Add("a");
            Assert.AreEqual(1, trie.Size());
            trie.Add("aa");
            Assert.AreEqual(2, trie.Size());
            trie.Add("aaa");
            Assert.AreEqual(3, trie.Size());
            trie.Add("aab");
            Assert.AreEqual(4, trie.Size());
            trie.Add("aac");
            Assert.AreEqual(5, trie.Size());

            trie.Add("a");
            trie.Add("aa");
            trie.Add("aaa");
            trie.Add("aab");
            trie.Add("aac");
            Assert.AreEqual(5, trie.Size());
        }

        [TestMethod()]
        public void HowManyStartsWithPrefixTest()
        {
            ITrie trie = new Trie.Trie();
            trie.Add("a");
            trie.Add("aa");
            trie.Add("aaa");
            trie.Add("aab");
            trie.Add("aac");
            Assert.AreEqual(5, trie.HowManyStartsWithPrefix(""));
            Assert.AreEqual(5, trie.HowManyStartsWithPrefix("a"));
            Assert.AreEqual(4, trie.HowManyStartsWithPrefix("aa"));
            Assert.AreEqual(1, trie.HowManyStartsWithPrefix("aab"));
            Assert.AreEqual(0, trie.HowManyStartsWithPrefix("aabz"));
        }

        [TestMethod()]
        public void TrieMassAddRemoveTest()
        {
            ITrie trie = new Trie.Trie();
            String s1 = "asdasdyu2ge12bd1ui2re98fjauifbsdjkfnn21342342312asdfasfas";
            String s2 = "zasfqr32jrb23hjrb2j3r23r32r23r23r32r";
            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Add(s1.Substring(0, i));
                Assert.AreEqual(i, trie.Size());
            }

            int curSize = trie.Size();
            for (var i = 1; i <= s2.Length; ++i)
            {
                trie.Add(s2.Substring(0, i));
                Assert.AreEqual(curSize + i, trie.Size());
            }

            curSize = trie.Size();

            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Add(s1.Substring(0, i));
                Assert.AreEqual(curSize, trie.Size());
            }

            for (var i = 1; i <= s1.Length; ++i)
            {
                trie.Remove(s1.Substring(0, i));
            }

            for (var i = 1; i <= s2.Length; ++i)
            {
                trie.Remove(s2.Substring(0, i));
            }

            Assert.AreEqual(0, trie.Size());
        }
    }
}