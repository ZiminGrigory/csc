using Mini_Roguelike;
using NUnit.Framework;

namespace UnitTestRogueLike
{
    [TestFixture]
    public class TestRogue
    {
        private Rogue mRogue { get; set; }

        [SetUp]
        public void Init()
        {
            mRogue = new Rogue(new Point{ X = 0, Y = 0 });
        }

        [Test]
        public void TesstMoveTo()
        {
            mRogue.MoveTo(new Point { X = 1, Y = 1 });
            Assert.AreEqual(mRogue.Position, new Point {X = 1, Y = 1});
        }

        [Test]
        public void TestLeft()
        {
            Assert.AreEqual(mRogue.NextPointIfMove(Directions.Left), new Point { X = 0, Y = -1 });
        }

        [Test]
        public void TestRight()
        {
            Assert.AreEqual(mRogue.NextPointIfMove(Directions.Right), new Point { X = 0, Y = 1 });
        }

        [Test]
        public void TestForward()
        {
            Assert.AreEqual(mRogue.NextPointIfMove(Directions.Forward), new Point { X = -1, Y = 0 });
        }

        [Test]
        public void TestBackward()
        {
            Assert.AreEqual(mRogue.NextPointIfMove(Directions.Backward), new Point {X = 1, Y = 0});
        }
    }
}
