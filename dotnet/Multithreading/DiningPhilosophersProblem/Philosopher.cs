using System.Threading;

namespace DiningPhilosophersProblem
{
    public class Philosopher
    {
        private readonly Fork _leftFork;
        private readonly Fork _rightFork;
        private int Id { get; }

        public Philosopher(int id, Fork left, Fork right)
        {
            Id = id;
            _leftFork = left;
            _rightFork = right;
        }

        public void MakePhilosophicalThings()
        {
            while (true)
            {
                System.Console.WriteLine($"Philosopher {Id} wait for left fork with id = {_leftFork.Id}");
                lock (_leftFork)
                {
                    System.Console.WriteLine($"Philosopher {Id} wait for right fork with id = {_rightFork.Id}");
                    lock (_rightFork)
                    {
                        System.Console.WriteLine($"Philosopher {Id} eats");
                        Thread.Sleep(1000);
                        Thread.Yield();
                    }
                }
            }
        }
    }
}