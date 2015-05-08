using System;
using OpenTK.Graphics.OpenGL;

namespace TheMoveToCSharp
{
    internal struct Vertex
    {
        public const int STRIDE = 12;
        public float X, Y, Z;

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    public class Mesh : IDisposable
    {
        private int _eboId;
        private int _vboId;

        private readonly ushort[] _indices =
        {
                0, 1, 2
        };

        private readonly Vertex[] _vertices =
        {
                //new Vertex(-0.5f, -0.5f, 0f),
                //new Vertex(0f, 0.5f, 0f),
                //new Vertex(-0.5f, -0.5f, 0f),

                new Vertex(0.0f, 0.0f, 0.0f),
                new Vertex(100f, 0.0f, 0f),
                new Vertex(0.0f, -100.0f, 0f)
        };

        public void InitializeVertexBuffers()
        {
            GL.GenBuffers(1, out _vboId);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboId);

            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(_vertices.Length * Vertex.STRIDE), _vertices, BufferUsageHint.StaticDraw);

            // Should this be put in a finally?
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            GL.GenBuffers(1, out _eboId);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, _eboId);

            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(_indices.Length * sizeof(ushort)), _indices, BufferUsageHint.StaticDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void Render()
        {
            GL.PushMatrix();

            //So you can see it onscreen :P
            GL.Translate(-50, 50, -100);

            //We dont have color data so it will resort to the last use GL_Color
            GL.Color3(1.0f, 1.0f, 1.0f);

            GL.EnableClientState(ArrayCap.VertexArray);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vboId);

            GL.VertexPointer(3, VertexPointerType.Float, Vertex.STRIDE, 0);

            GL.DrawElements(BeginMode.Triangles, _indices.Length, DrawElementsType.UnsignedShort, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.DisableClientState(ArrayCap.VertexArray);

            GL.PopMatrix();
        }

        public void Dispose()
        {
            //GL.DeleteBuffers();
        }
    }
}