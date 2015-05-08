using System;
using System.ComponentModel;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace OpenTKBasics
{
    public class Game
    {
        private Vertex[] _vertexBuffer;
        private int _vertexBufferObject;
        public Texture2D Texture2D { get; set; }
        public GameWindow Window { get; set; }

        public Game(GameWindow gameWindow)
        {
            Window = gameWindow;

            RegisterForEvents();
        }

        private static void DrawOldSchoolVertexes()
        {
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
        }

        private void DrawVertexBuffers()
        {
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            // The order of the pointers matter on how the struct is order is right now color, position, texCoord

            GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)(0)); // Step over Position and Texture
            GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector4.SizeInBytes));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector4.SizeInBytes + Vector2.SizeInBytes));
            

            //GL.Color3(Color.BlueViolet);
            GL.DrawArrays(PrimitiveType.Quads, 0, _vertexBuffer.Length);
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

            var world = Matrix4.CreateOrthographicOffCenter(0, Window.Width, Window.Height, 0, 0, 1);
            GL.MatrixMode(MatrixMode.Modelview);

            GL.LoadMatrix(ref world);

            //DrawOldSchoolVertexes();

            DrawVertexBuffers();

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
                                    new Vertex(new Vector2(0, 0), new Vector2(0, 0)),
                                    new Vertex(new Vector2(300, 0), new Vector2(1, 0)),
                                    new Vertex(new Vector2(300, 300), new Vector2(1, 1))
                                    {
                                            Color = Color.Red
                                    },
                                    new Vertex(new Vector2(0, 300), new Vector2(0, 1)),
                                    //new Vertex(new Vector2(200, 0), new Vector2(0, 0)),
                                    //new Vertex(new Vector2(200, 100), new Vector2(0, 1))
                            };

            _vertexBufferObject = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.SizeInBytes * _vertexBuffer.Length), _vertexBuffer, BufferUsageHint.StaticDraw);

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