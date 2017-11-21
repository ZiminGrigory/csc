using System;
using System.IO;
using NUnit.Framework;
using Mini_Roguelike;

namespace UnitTestRogueLike
{
    [TestFixture]
    public class TestMap
    {
        private Map _map;

        [Test]
        public void TestOpenMap([Values ("\\testMap\\testMap.txt", "\\testMap\\testMap2.txt")] string path)
        {
            var dir = new FileInfo(Path.GetDirectoryName(new Uri(typeof(TestMap).Assembly.CodeBase).LocalPath));
            _map = new Map(File.OpenText(dir.Directory.Parent.FullName + path));
        }

        [Test]
        public void TestIsPosFree1()
        {
            var dir = new FileInfo(Path.GetDirectoryName(new Uri(typeof(TestMap).Assembly.CodeBase).LocalPath));
            _map = new Map(File.OpenText(dir.Directory.Parent.FullName + "\\testMap\\testMap.txt"));
            Assert.False(_map.IsPosFree(new Point { X = 0, Y = 1 }));
        }

        [Test]
        public void TestIsPosFree2()
        {
            var dir = new FileInfo(Path.GetDirectoryName(new Uri(typeof(TestMap).Assembly.CodeBase).LocalPath));
            _map = new Map(File.OpenText(dir.Directory.Parent.FullName + "\\testMap\\testMap.txt"));
            Assert.True(_map.IsPosFree(new Point { X = 1, Y = 1}));
        }

        [Test]
        public void TestGetRoguePoint()
        {
            var dir = new FileInfo(Path.GetDirectoryName(new Uri(typeof(TestMap).Assembly.CodeBase).LocalPath));
            _map = new Map(File.OpenText(dir.Directory.Parent.FullName + "\\testMap\\testMap.txt"));
            Point p = null;
            _map.GetRoguePoint(ref p);
            Assert.AreEqual(p, new Point { X = 2, Y = 1 });
        }

        [Test]
        public void TestPrint()
        {
            var dir = new FileInfo(Path.GetDirectoryName(new Uri(typeof(TestMap).Assembly.CodeBase).LocalPath));
            _map = new Map(File.OpenText(dir.Directory.Parent.FullName + "\\testMap\\testMap2.txt"));
            _map.PrintMapToConsole(false, 0, 0);
        }
    }
}