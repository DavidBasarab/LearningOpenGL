using System.Runtime.InteropServices;
using System.Security;

namespace TheMoveToCSharp
{
    public class PreciseTimer
    {
        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool QueryPerformanceFrequency(out long frequency);

        private long _previousElapsedTime;
        private readonly long _ticksPerSecond;

        public PreciseTimer()
        {
            QueryPerformanceFrequency(out _ticksPerSecond);

            GetElapsedTime();
        }

        public double GetElapsedTime()
        {
            long time;

            QueryPerformanceCounter(out time);

            var elapsedTime = (time - _previousElapsedTime) / (double)_ticksPerSecond;

            _previousElapsedTime = time;

            return elapsedTime;
        }
    }
}