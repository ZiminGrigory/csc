using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BlockingArrayQueue;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class BlockingArrayQueueTest
    {
        private IBlockingArrayQueue<int> _queue;
        private const int MaxSize = 5;

        [SetUp]
        public void InitTest()
        {
            //_queue = new LockBasedBlockingArrayQueue<int>(MaxSize);
            _queue = new LockFreeBlockingArrayQueue<int>(MaxSize);
        }

        [Test]
        public void TestEmpty()
        {
            Assert.AreEqual(0, _queue.Size());
        }

        [Test]
        public void TestAddOne()
        {
            _queue.Enqueue(5);
            Assert.AreEqual(5, _queue.Dequeue());
        }

        [Test]
        public void TestTryEnqueue()
        {
            _queue.Enqueue(1);
            _queue.Enqueue(1);
            _queue.Enqueue(1);
            _queue.Enqueue(1);
            _queue.Enqueue(1);
            Assert.False(_queue.TryEnqueue(1));
        }

        [Test]
        public void TestTryDequeueOnEmpty()
        {
            Assert.False(_queue.TryDequeue(out int a));
        }

        [Test]
        public void TestTryDequeue()
        {
            int a;
            _queue.Enqueue(1);
            Assert.True(_queue.TryDequeue(out a));
            Assert.AreEqual(a, 1);
        }

        [Test]
        public void TestMultyThreads()
        {
            var threads = new List<Thread>();
            foreach (var i in Enumerable.Range(1, 20))
            {
                threads.Add(new Thread(_ => _queue.Enqueue(i)));
            }

            foreach (var i in Enumerable.Range(1, 20))
            {
                threads.Add(new Thread(_ => _queue.Dequeue()));
            }

            foreach (var thr in threads)
            {
                thr.Start();
            }

            Thread.Sleep(5000);

            Assert.AreEqual(_queue.Size(), 0);
        }
    }
}