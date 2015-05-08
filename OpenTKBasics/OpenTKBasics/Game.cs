using System;
using System.ComponentModel;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKBasics
{
    public class Game
    {
        private Vector2[] _vertexBuffer;
        private int _vertexBufferObject;
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

            Matrix4 world = Matrix4.CreateOrthographicOffCenter(0, Window.Width, Window.Height, 0, 0, 1);

            GL.LoadMatrix(ref world);

            GL.Begin(PrimitiveType.Quads);

            // 0, 0 Center
            // -1 Right, 1, Left
            GL.Color3(Color.SpringGreen);

            GL.TexCoord2(0, 0);
            GL.Vertex2(0, 0);

            GL.TexCoord2(1, 0);
            GL.Vertex2(100, 0);

            GL.TexCoord2(1, 1);
            GL.Vertex2(100, 100);

            GL.TexCoord2(0, 1);
            GL.Vertex2(0, 100);

            GL.End();

            
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.VertexPointer(2, VertexPointerType.Float, Vector2.SizeInBytes * 2, 0);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vector2.SizeInBytes * 2, Vector2.SizeInBytes);

            GL.Color3(Color.BlueViolet);
            GL.DrawArrays(PrimitiveType.Quads, 0, _vertexBuffer.Length / 2);

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

            _vertexBuffer = new[]
                            {
                                    new Vector2(0, 100), new Vector2(0, 0), 
                                    new Vector2(100, 100), new Vector2(1, 0), 
                                    new Vector2(100, 200),new Vector2(1, 1), 
                                    new Vector2(0, 200),new  Vector2(0, 1), 
                            };

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vector2.SizeInBytes * _vertexBuffer.Length), _vertexBuffer, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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