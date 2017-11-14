using System;

namespace Mini_Roguelike
{
    class ProgramHW
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Should be exactly one argument -- path to map");
                Console.ReadKey();
            }
            else
            {
                try
                {
                    var game = new Game(args[0]);
                    var eventLoop = new EventLoop();
                    eventLoop.Left += game.HandleLeft;
                    eventLoop.Right += game.HandleRight;
                    eventLoop.Forward += game.HandleForward;
                    eventLoop.Backward += game.HandleBackward;
                    eventLoop.Run();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine(e.Message);
                    throw;
                }
            }
        }
    }
}
