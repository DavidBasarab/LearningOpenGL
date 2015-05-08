using System;
using System.ComponentModel;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;

namespace OpenTKBasics
{
    public class Game
    {
        public GameWindow Window { get; set; }

        public Game(GameWindow gameWindow)
        {
            Window = gameWindow;

            RegisterForEvents();
        }

        private void OnClosing(object sender, CancelEventArgs e) {}

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Flush();

            Window.SwapBuffers();
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            // e.Time Time in Seconds since last update
        }

        private void OnWindowLoad(object sender, EventArgs e)
        {
            GL.ClearColor(Color4.Coral);
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