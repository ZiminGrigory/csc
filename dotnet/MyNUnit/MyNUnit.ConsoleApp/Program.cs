using System;
using System.IO;

namespace MyNUnit.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Expected one argument: path to dll-s");
                Console.ReadKey();
                return;
            }

            if (Directory.Exists(args[0]))
            {
                foreach (var assembly in Directory.GetFiles(args[0]))
                {
                    MyNUnitRunner.RunTestsInAssembly(assembly, Console.Out, true);
                }

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine("Expected one argument: path to dll-s (directory does not exist)");
                Console.ReadKey();
            }
        }
    }
}