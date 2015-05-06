using System;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace TheMoveToCSharp
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            var timer = new PreciseTimer();

            RunSample();

            //RunGenericGame(timer);

            //StartGameLoop();

            //Console.WriteLine("Press any key to exit");

            //Console.ReadKey(false);
        }

        private static void RunGenericGame(PreciseTimer timer)
        {
            using (var game = new GameWindow(800, 600))
            {
                game.Load += (sender, e) =>
                             {
                                 // setup settings, load textures, sounds
                                 game.VSync = VSyncMode.On;
                             };

                game.Resize += (sender, e) => { GL.Viewport(0, 0, game.Width, game.Height); };

                game.UpdateFrame += (sender, e) => { if (game.Keyboard[Key.Escape]) game.Exit(); };

                var counter = 0.0f;
                var blue = 0.3f;
                var red = 0.0f;

                game.RenderFrame += (sender, e) =>
                                    {
                                        counter++;

                                        Console.WriteLine("Counter = {0}", counter);

                                        if (counter % 10 == 0)
                                        {
                                            blue += 0.1f;

                                            if (blue > 1.0f) blue = 0.03f;

                                            red += 0.1f;

                                            if (red > 1.0f) red = 0.0f;
                                        }

                                        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                                        GL.MatrixMode(MatrixMode.Projection);

                                        GL.LoadIdentity();

                                        GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);

                                        var elapsedTime = timer.GetElapsedTime();

                                        var sinOfTime = (float)Math.Abs(Math.Sin(counter));

                                        var time = (float)e.Time;

                                        GL.ClearColor(red, 0.0f, blue, 1.0f);

                                        GL.Clear(ClearBufferMask.ColorBufferBit);

                                        //GL.Begin(BeginMode.Triangles);

                                        //GL.Color3(Color.MidnightBlue);
                                        //GL.Vertex2(-1.0f, 1.0f);

                                        //GL.Color3(Color.SpringGreen);
                                        //GL.Vertex2(0.0f, -1.0f);

                                        //GL.Color3(Color.Ivory);
                                        //GL.Vertex2(1.0f, 1.0f);

                                        //GL.End();

                                        game.SwapBuffers();
                                    };

                game.Run(60.0);
            }
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