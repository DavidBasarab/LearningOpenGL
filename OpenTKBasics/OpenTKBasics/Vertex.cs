using System.Drawing;
using System.Runtime.InteropServices;
using OpenTK;

namespace OpenTKBasics
{
    public struct Vertex
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
}