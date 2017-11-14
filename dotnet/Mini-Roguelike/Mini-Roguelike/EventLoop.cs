using System;
using System.Threading.Tasks;

namespace Mini_Roguelike
{
    public class EventLoop
    {
        public Action Left { get; set; }
        public Action Right { get; set; }
        public Action Forward { get; set; }
        public Action Backward { get; set; }

        public void Run()
        {
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.Clear();
                Console.WriteLine("Exiting...");
                Environment.Exit(0);
            };

            var taskKeys = new Task(ReadKeys);
            taskKeys.Start();

            var tasks = new[] { taskKeys };
            Task.WaitAll(tasks);
        }

        private void ReadKeys()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            while (key.Key != ConsoleKey.Escape)
            {

                key = Console.ReadKey(true);

                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        Left();
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        Right();
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        Forward();
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        Backward();
                        break;
                    case ConsoleKey.Escape:
                        break;
                }
            }
        }
    }
}