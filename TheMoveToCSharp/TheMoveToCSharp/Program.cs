using System;
using System.Threading.Tasks;

namespace TheMoveToCSharp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartGameLoop();

            Console.WriteLine("Press any key to exit");

            Console.ReadKey(false);
        }

        private static void RunSample()
        {
            var display = new Display(800, 600);

            display.Run();
        }

        private static void StartGameLoop()
        {
            Task.Run(() => StartOpenGl());
        }

        private static void StartOpenGl()
        {
            try
            {
                RunSample();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:      {0}", ex.Message);
                Console.WriteLine("StackTrack: {0}", ex.StackTrace);
            }
        }
    }
}