using System;
using System.Diagnostics;

namespace Minesweeper.Infrastructure
{
    public class MethodTimer : IDisposable
    {
        private readonly Stopwatch _watch;

        public MethodTimer()
        {
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _watch.Stop();
        }

        public long Time()
        {
            return _watch.ElapsedMilliseconds;
        }

        public string Time(string header)
        {
            return $"{header}: {Time()}";
        }
    }
}