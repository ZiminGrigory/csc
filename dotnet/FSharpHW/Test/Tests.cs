using System.Collections.Generic;
using System.Linq;
using Microsoft.FSharp.Collections;
using NUnit.Framework;
using FSharpHW;
using System.Numerics;

namespace Test
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void TestFibTenth()
        {
            Assert.AreEqual(HW.fib(10), (BigInteger) 55);
        }

        [Test]
        public void TestFibFirst()
        {
            Assert.AreEqual(HW.fib(1), (BigInteger) 1);
        }

        [Test]
        public void TestFibSecond()
        {
            Assert.AreEqual(HW.fib(2), (BigInteger) 1);
        }

        [Test]
        public void TestReverseList()
        {
            var intsList = new List<int> { 1, 2, 3 };
            System.Console.WriteLine(ListModule.OfSeq(intsList));
            var intsReversedList = new List<int> { 3, 2, 1 };
            var fsharpList = ListModule.OfSeq(intsList);
            var fsharpReversedList = ListModule.OfSeq(intsReversedList);
            var res = HW.reverse(fsharpList);
            System.Console.WriteLine(ListModule.OfSeq(res));
            Assert.AreEqual(res, fsharpReversedList);
        }

        [Test]
        public void TestReverseEmptyList()
        {
            var fsharpList = ListModule.OfSeq(new List<int>());
            var fsharpSortedList = ListModule.OfSeq(new List<int>());
            var res = HW.reverse(fsharpList);
            Assert.AreEqual(fsharpSortedList, res);
        }

        [Test]
        public void TestMergeSortEmpty()
        {
            var fsharpList = ListModule.OfSeq(new List<int>());
            var fsharpSortedList = ListModule.OfSeq(new List<int>());
            var res = HW.mergeSort(fsharpList);
            Assert.AreEqual(fsharpSortedList, res);
        }

        [Test]
        public void TestMergeSortOneElement()
        {
            var fsharpList = ListModule.OfSeq(new List<int> { 1 });
            var fsharpSortedList = ListModule.OfSeq(new List<int> { 1 });
            var res = HW.mergeSort(fsharpList);
            Assert.AreEqual(fsharpSortedList, res);
        }

        [Test]
        public void TestMergeSort()
        {
            var intsList = new List<int> { -10, 213, 2, 0, -1, 234, -34, 5, 232, 7, 6, 5, 4, 3, 2 , 1 };
            var fsharpList = ListModule.OfSeq(intsList);
            var fsharpSortedList =
                ListModule.OfSeq(new List<int> { -34, -10, -1, 0, 1, 2, 2, 3, 4, 5, 5, 6, 7, 213, 232, 234 });

            var res = HW.mergeSort(fsharpList);
            foreach (var i in ListModule.OfSeq(res)) {
                System.Console.Write(i);
                System.Console.Write(" ");
            }

            Assert.AreEqual(fsharpSortedList, res);
        }

        [Test]
        public void TestPrimes()
        {
            var res = new BigInteger[] {
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101,
                103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211,
                223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337,
                347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461,
                463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601,
                607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739,
                743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881,
                883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997
            };

            var first168 = HW.primeNumbers.Take(168);
            Assert.AreEqual(first168, res);
        }

        [Test]
        public void TestArythmeticTree()
        {
            var a = HW.ArythmeticsTree.NewAdd(HW.ArythmeticsTree.NewValue(24.0), HW.ArythmeticsTree.NewValue(2.4));
            var b = HW.ArythmeticsTree.NewSub(HW.ArythmeticsTree.NewValue(24.0), HW.ArythmeticsTree.NewValue(2.4));
            var c = HW.ArythmeticsTree.NewDiv(a, b);
            var res = HW.evaluate(c);

            System.Console.Write(res);
            Assert.IsTrue(res - 1.222222222222 <= 0.00000001);
        }
    }
}