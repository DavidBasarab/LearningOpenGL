using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace TheMoveToCSharp
{
    public class Sample
    {
        private const float STARTING_BLUE = 0.3f;
        private const float STARTING_RED = 0.0f;
        private float _blue;
        private int _loopCount;
        private float _red;
        private readonly GameWindow _gameWindow;
        public int Height { get; private set; }
        public int Width { get; private set; }

        public Sample(int width, int height)
        {
            Width = width;
            Height = height;

            _gameWindow = new GameWindow(Width, Height, GraphicsMode.Default);

            _gameWindow.Title = "Moving to CSharp Window";

            _gameWindow.UpdateFrame += OnUpdateFrame;
            _gameWindow.RenderFrame += OnRenderFrame;
            _gameWindow.Load += OnLoad;
            _gameWindow.Resize += OnResize;

            _red = STARTING_RED;
            _blue = STARTING_BLUE;
        }

        public void Run()
        {
            _gameWindow.Run(60, 60);
        }

        private void DetermineBackGroundColor()
        {
            if (_loopCount % 10 == 0)
            {
                _red += 0.1f;
                _blue += 0.1f;

                if (_red > 1.0f) _red = STARTING_RED;

                if (_blue > 1.0f) _blue = STARTING_BLUE;
            }

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);

            GL.ClearColor(_red, 0.14f, _blue, 1.0f);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            _gameWindow.VSync = VSyncMode.On;

            var shader = new Shader();

            var vertexShader = new ShaderProgram()
            {
                LocationToShader = string.Format(@"{0}\Resources\BasicVertexShader.vsl", Environment.CurrentDirectory),
                ShaderType = ShaderType.VertexShader
            };

            var fragmentShader = new ShaderProgram()
            {
                LocationToShader = string.Format(@"{0}\Resources\BasicFragmentShader.fsl", Environment.CurrentDirectory),
                ShaderType = ShaderType.FragmentShader
            };

            var shadersLoaded = shader.LoadShaders(vertexShader, fragmentShader);

        }

        private void OnRenderFrame(object sender, FrameEventArgs e)
        {
            _loopCount++;

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            DetermineBackGroundColor();

            _gameWindow.SwapBuffers();
        }

        private void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, _gameWindow.Width, _gameWindow.Height);
        }

        private void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            if (_gameWindow.Keyboard[Key.Escape]) _gameWindow.Exit();
        }
    }
}