using OpenTK;

namespace OpenTKBasics
{
    public struct Texture2D
    {
        private readonly int _id;
        private readonly Vector2 _size;

        public int Height
        {
            get { return (int)Size.Y; }
        }

        public int Id
        {
            get { return _id; }
        }

        public Vector2 Size
        {
            get { return _size; }
        }

        public int Width
        {
            get { return (int)Size.X; }
        }

        public Texture2D(int id, Vector2 size)
        {
            _size = size;

            _id = id;
        }
    }
}