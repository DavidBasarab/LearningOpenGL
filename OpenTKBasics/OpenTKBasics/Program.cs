using System;
using OpenTK;

namespace OpenTKBasics
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var window = new GameWindow(800, 600);

            var game = new Game(window);

            window.Run();

            //Console.WriteLine("Press any key to exit. . . . . .");

            //Console.ReadKey(false);
        }
    }
}