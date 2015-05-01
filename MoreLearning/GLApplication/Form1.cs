using System.Windows.Forms;
using MoreLearning;
using OpenTK.Graphics.ES10;

namespace GLApplication
{
    public partial class Form1 : Form
    {
        private FastLoop _fastLoop;
        private readonly bool _fulllScreen = false;

        public Form1()
        {
            _fastLoop = new FastLoop(GameLoop);

            InitializeComponent();

            if (_fulllScreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
        }

        private void GameLoop(double elapsedTime)
        {
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            GL.Clear((uint)ClearBufferMask.ColorBufferBit);

            
        }
    }
}