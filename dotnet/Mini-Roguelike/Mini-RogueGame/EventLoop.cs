using System;

namespace Mini_RogueGame
{
    public class EventLoop
    {
        public void Run(Action left, Action right, Action forward, Action backward)
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.LeftArrow:
                        left();
                        break;
                    case ConsoleKey.RightArrow:
                        right();
                        break;
                    case ConsoleKey.UpArrow:
                        forward();
                        break;
                    case ConsoleKey.DownArrow:
                        backward();
                        break;
                    case ConsoleKey.A:
                        left();
                        break;
                    case ConsoleKey.D:
                        right();
                        break;
                    case ConsoleKey.W:
                        forward();
                        break;
                    case ConsoleKey.S:
                        backward();
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}