using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trie;

namespace TrieTests
{
    [TestClass]
    public class TrieTests
    {
        private ITrie TrieForTest { get; set; }
        private readonly string[] _baseSourceString = {"a", "aa", "aaa", "aab", "aac"};

        [TestCleanup]
        public void TestCleanup()
        {
            TrieForTest = null;
        }

        [TestInitialize]
        public void TestInit()
        {
            TrieForTest = new Trie.Trie();
        }

        [TestMethod]
        public void TrieTest()
        {
            Assert.IsNotNull(TrieForTest);
        }

        [TestMethod]
        public void AddTest()
        {
            bool result = TrieForTest.Add("a");
            bool result1 = TrieForTest.Add("aa");
            bool result2 = TrieForTest.Add("aaa");
            bool result3 = TrieForTest.Add("aab");
            bool result4 = TrieForTest.Add("aac");

            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.IsTrue(result4);
        }

        [TestMethod]
        public void ContainsTest()
        {
            foreach (var str in _baseSourceString)
            {
                TrieForTest.Add(str);
            }

            Assert.IsFalse(TrieForTest.Contains(""));
            foreach (var str in _baseSourceString)
            {
                Assert.IsTrue(TrieForTest.Contains(str));
            }
        }

        [TestMethod]
        public void RemoveTest()
        {
            foreach (var str in _baseSourceString)
            {
                TrieForTest.Add(str);
            }

            bool result = TrieForTest.Remove("a");
            bool result1 = TrieForTest.Remove("a");
            bool result2 = TrieForTest.Remove("");

            Assert.IsTrue(result);
            Assert.IsFalse(result1);
            Assert.IsFalse(result2);
        }

        [TestMethod]
        public void SizeTest()
        {
            for (var i = 0; i < 42; ++i)
            {
                foreach (var str in _baseSourceString)
                {
                    TrieForTest.Add(str);
                }

                Assert.AreEqual(5, TrieForTest.Size());
            }
        }

        [TestMethod]
        public void TestHowManyStartsWithPrefix()
        {
            foreach (var str in _baseSourceString)
            {
                TrieForTest.Add(str);
            }

            int result = TrieForTest.HowManyStartsWithPrefix("");
            int result1 = TrieForTest.HowManyStartsWithPrefix("a");
            int result2 = TrieForTest.HowManyStartsWithPrefix("aa");
            int result3 = TrieForTest.HowManyStartsWithPrefix("aab");
            int result4 = TrieForTest.HowManyStartsWithPrefix("aabz");
            Assert.AreEqual(5, result);
            Assert.AreEqual(5, result1);
            Assert.AreEqual(4, result2);
            Assert.AreEqual(1, result3);
            Assert.AreEqual(0, result4);
        }

        [TestMethod]
        public void TestTrieMassAddRemove()
        {
            const string s1 = "asdasdyu2ge12bd1ui2re98fjauifbsdjkfnn21342342312asdfasfas";
            const string s2 = "zasfqr32jrb23hjrb2j3r23r32r23r23r32r";
            int idealSize = 0;

            // mass adding
            for (var i = 1; i <= s1.Length; ++i)
            {
                TrieForTest.Add(s1.Substring(0, i));
                ++idealSize;
            }

            for (var i = 1; i <= s2.Length; ++i)
            {
                TrieForTest.Add(s2.Substring(0, i));
                ++idealSize;
            }

            for (var i = 1; i <= s1.Length; ++i)
            {
                TrieForTest.Add(s1.Substring(0, i));
            }

            int sizeAfterMassAdding = TrieForTest.Size();

            Assert.AreEqual(idealSize, sizeAfterMassAdding);

            // mass removing
            for (var i = 1; i <= s1.Length; ++i)
            {
                TrieForTest.Remove(s1.Substring(0, i));
            }

            for (var i = 1; i <= s2.Length; ++i)
            {
                TrieForTest.Remove(s2.Substring(0, i));
            }

            int sizeAfterMassRemoving = TrieForTest.Size();

            Assert.AreEqual(0, sizeAfterMassRemoving);
        }
    }
}