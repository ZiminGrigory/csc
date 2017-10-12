using System;
using NUnit.Framework;
using OptionHW;

namespace OptionTests
{
    [TestFixture]
    public class OptionTests
    {
        [Test]
        public void TestSome()
        {
            Option<int> option = Option.Some(42);
            bool res = option.IsSome;
            Assert.IsTrue(res);
        }

        [Test]
        public void TestNone()
        {
            Option<int> option = Option<int>.None();
            bool res = option.IsNone;
            Assert.IsTrue(res);
        }

        [Test]
        public void TestValue()
        {
            Option<int> option = Option.Some(42);
            int res = option.Value;
            Assert.AreEqual(42, res);
        }

        [Test]
        public void TestValueException()
        {
            var option = Option<int>.None();
            Assert.That(() => option.Value, Throws.TypeOf<NullReferenceException>());
        }

        [Test]
        public void TestMap0()
        {
            Option<int> option = Option.Some(42);
            Option<int> nOption = option.Map(x => x * 2);
            Assert.AreEqual(42 * 2, nOption.Value);
        }

        [Test]
        public void TestMap1()
        {
            var left = Option.Some(2).Map(x => x * 2);
            var right = Option.Some(4);
            Assert.IsTrue(Equals(left, right));
        }

        [Test]
        public void TestMap2()
        {
            Assert.IsTrue(Equals(Option<int>.None().Map(x => x * 2), Option<int>.None()));
        }

        [Test]
        public void TestFlatten()
        {
            var inner = Option.Some(2);
            var outer = Option.Some(inner);
            Assert.IsTrue(Equals(Option<int>.Flatten(outer), inner));
        }
    }
}
