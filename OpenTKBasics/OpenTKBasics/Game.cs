using System;
using System.ComponentModel;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKBasics
{
    public class Game
    {
        public Texture2D Texture2D { get; set; }
        public GameWindow Window { get; set; }

        public Game(GameWindow gameWindow)
        {
            Window = gameWindow;

            RegisterForEvents();
        }

        private void OnClosing(object sender, CancelEventArgs e) {}

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            //GL.ClearColor(Color4.DarkBlue);
            GL.ClearColor(Color.FromArgb(5, 5, 25));
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            GL.Begin(PrimitiveType.Triangles);

            // 0, 0 Center
            // -1 Right, 1, Left
            GL.Color3(Color.Red);

            GL.TexCoord2(0, 0);

            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 0);

            GL.Vertex2(1, 1);

            GL.Color4(Color.FromArgb(0, Color.Red));

            GL.TexCoord2(1, 1);

            GL.Vertex2(-1, 1);

            GL.End();

            GL.Flush();

            Window.SwapBuffers();
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            // e.Time Time in Seconds since last update
        }

        private void OnWindowLoad(object sender, EventArgs e)
        {
            Texture2D = ContentPipe.LoadTexture2D("peguin2.jpg", false);
        }

        private void RegisterForEvents()
        {
            Window.Load += OnWindowLoad;
            Window.RenderFrame += OnRenderFrame;
            Window.UpdateFrame += OnUpdateFrame;
            Window.Closing += OnClosing;
        }
    }
}