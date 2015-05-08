using System;
using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TheMoveToCSharp
{
    internal struct Vertex
    {
        public Vector4 color;
        public Vector2 position;
        public Vector2 texCoord;

        public Color Color
        {
            get { return Color.FromArgb((int)(255 * color.W), (int)(255 * color.X), (int)(255 * color.Y), (int)(255 * color.Z)); }
            set { color = new Vector4(value.R / 255f, value.G / 255f, value.B / 255f, value.A / 255f); }
        }

        public static int SizeInBytes
        {
            get { return Marshal.SizeOf(typeof(Vertex)); }
        }

        public Vertex(Vector2 position, Vector2 texCoord)
        {
            this.position = position;
            this.texCoord = texCoord;
            color = new Vector4(1, 1, 1, 1);
        }
    }

    public class Mesh : IDisposable
    {
        private uint[] _indexBuffer;
        private int _indexBufferId;
        private Vertex[] _vertexBuffer;
        private int _vertexBufferId;

        public void Dispose()
        {
            //GL.DeleteBuffers();
        }

        public void InitializeVertexBuffers()
        {
            _vertexBuffer = new[]
                            {
                                    new Vertex(new Vector2(0, 0), new Vector2(0, 0)),
                                    new Vertex(new Vector2(300, 0), new Vector2(1, 0)),
                                    new Vertex(new Vector2(300, 300), new Vector2(1, 1))
                                    {
                                            Color = Color.FromArgb(0, Color.DarkViolet)
                                    },
                                    new Vertex(new Vector2(0, 300), new Vector2(0, 1))
                                    {
                                            Color = Color.FromArgb(0, Color.Goldenrod)
                                    },
                                    new Vertex(new Vector2(0, 500), new Vector2(0, 0)),
                                    new Vertex(new Vector2(300, 500), new Vector2(1, 0))
                            };

            _indexBuffer = new uint[]
                           {
                                   0, 1, 2, 3
                                   //4, 5, 2, 3
                           };

            _vertexBufferId = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferId);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertex.SizeInBytes * _vertexBuffer.Length), _vertexBuffer, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            _indexBufferId = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferId);

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(sizeof(uint) * _indexBuffer.Length), _indexBuffer, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Render()
        {
            var world = Matrix4.CreateTranslation(200, 200, 0);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref world);

            GL.EnableClientState(ArrayCap.ColorArray);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferId);

            GL.ColorPointer(4, ColorPointerType.Float, Vertex.SizeInBytes, (IntPtr)0);
            GL.VertexPointer(2, VertexPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector4.SizeInBytes));
            GL.TexCoordPointer(2, TexCoordPointerType.Float, Vertex.SizeInBytes, (IntPtr)(Vector4.SizeInBytes + Vector2.SizeInBytes));

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _indexBufferId);

            GL.DrawElements(PrimitiveType.Quads, _indexBuffer.Length, DrawElementsType.UnsignedInt, 0);

        }
    }
}