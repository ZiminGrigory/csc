using System.Linq;
using System.Threading;
using NUnit.Framework;
using DiningPhilosophersProblem;

namespace Tests
{
    [TestFixture]
    public class PhilosophersTest
    {
        [Test]
        public void TestPhilosophersDinner()
        {
            var fork1 = new Fork {Id = 1};
            var fork2 = new Fork { Id = 2 };
            var fork3 = new Fork { Id = 3 };
            var fork4 = new Fork { Id = 4 };
            var fork5 = new Fork { Id = 5 };

            var philosophers = new[]
            {
                new Philosopher(1, fork1, fork2)
                , new Philosopher(2, fork2, fork3)
                , new Philosopher(3, fork3, fork4)
                , new Philosopher(3, fork3, fork4)
                , new Philosopher(4, fork4, fork5)
                , new Philosopher(5, fork5, fork1)
            };

            var threads = philosophers.Select(philosopher => new Thread(philosopher.MakePhilosophicalThings)).ToList();
            threads.ForEach(thr => thr.Start());

            Thread.Sleep(6000);

            threads.ForEach(thr => thr.Abort());
        }
    }
}
