using System.Runtime.InteropServices;
using System.Security;

namespace MoreLearning
{
    public class PreciseTimer
    {
        private long _previousElapsedTime;
        private readonly long _ticksPerSecond;

        public PreciseTimer()
        {
            QueryPerformanceFrequency(ref _ticksPerSecond);
            GetElapsedTime();
        }

        private double CalculateElapsedTime(long time)
        {
            return (time - _previousElapsedTime) / (double)_ticksPerSecond;
        }

        public double GetElapsedTime()
        {
            long time = 0;

            QueryPerformanceCounter(ref time);

            var elapsedTime = CalculateElapsedTime(time);

            _previousElapsedTime = time;

            return elapsedTime;
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceCounter(ref long performanceCount);

        [SuppressUnmanagedCodeSecurity]
        [DllImport("kernel32")]
        private static extern bool QueryPerformanceFrequency(ref long performanceFrequency);
    }
}