using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace MoreLearning
{
    public class FastLoop
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        private readonly LoopCallback _callback;
        private readonly PreciseTimer _timer;

        public FastLoop(LoopCallback callback)
        {
            _timer = new PreciseTimer();

            _callback = callback;

            Application.Idle += OnApplicationEnterIdle;
        }

        private bool IsApplicationStillIdle()
        {
            Message msg;

            return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
        }

        private void OnApplicationEnterIdle(object sender, EventArgs e)
        {
            while (IsApplicationStillIdle()) _callback(_timer.GetElapsedTime());
        }
    }
}