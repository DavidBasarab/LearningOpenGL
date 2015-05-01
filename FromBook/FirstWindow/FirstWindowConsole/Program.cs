using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace FirstWindowConsole
{
    internal class Program
    {
        [STAThread]
        private static void Main()
        {
            using (var game = new GameWindow())
            {
                game.Load += (s, e) => game.VSync = VSyncMode.On;

                game.Resize += (s, e) => GL.Viewport(0, 0, game.Width, game.Height);

                game.UpdateFrame += (s, e) => { if (game.Keyboard[Key.Escape]) game.Exit(); };

                game.RenderFrame += (s, e) =>
                                    {
                                        GL.Clear((ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));

                                        GL.MatrixMode(MatrixMode.Projection);

                                        GL.LoadIdentity();

                                        GL.Ortho(-1.0f, 1.0f, -1.0f, 1.0f, 0.0f, 4.0f);

                                        GL.Begin(PrimitiveType.Triangles);

                                        GL.Color3(Color.MidnightBlue);
                                        GL.Vertex2(-1.0f, 1.0f);
                                        GL.Color3(Color.SpringGreen);
                                        GL.Vertex2(0.0f, -1.0f);
                                        GL.Color3(Color.Goldenrod);
                                        GL.Vertex2(1.0f, 1.0f);

                                        GL.End();

                                        game.SwapBuffers();
                                    };

                game.Run(60.0);
            }
        }
    }
}