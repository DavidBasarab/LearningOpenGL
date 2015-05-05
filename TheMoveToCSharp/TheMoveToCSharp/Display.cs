using OpenTK;
using OpenTK.Graphics;


namespace TheMoveToCSharp
{
    public class Display
    {
        private readonly GameWindow _gameWindow;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Display(int width, int height)
        {
            Width = width;
            Height = height;

            _gameWindow = new GameWindow(Width, Height, GraphicsMode.Default);

            _gameWindow.Title = "Moving to CSharp Window";

            
        }

        public void Run()
        {
            _gameWindow.Run(60, 60);
        }
    }
}