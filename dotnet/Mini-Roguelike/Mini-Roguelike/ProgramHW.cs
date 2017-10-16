using System;
using Mini_RogueGame;

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
                    eventLoop.Run(game.HandleLeft, game.HandleRight, game.HandleForward, game.HandleBackward);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}
