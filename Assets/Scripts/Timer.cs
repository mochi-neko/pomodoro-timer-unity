using System;

namespace Mochineko.Pomodoro
{
	public class Timer : IDisposable
	{
		private readonly TimeSpan span;
		private readonly System.Diagnostics.Stopwatch stopwatch;

		private static readonly TimeSpan OneSecond
			= new TimeSpan(hours: 0, minutes: 0, seconds: 1);

		public Timer(TimeSpan span)
		{
			this.span = span;
			this.stopwatch = new System.Diagnostics.Stopwatch();

			stopwatch.Start();
		}

		private TimeSpan Elapsed
			=> stopwatch.Elapsed;

		private TimeSpan Remaining
			=> span.Subtract(Elapsed);

		public TimeSpan RemainingRoundedUp
			=> span
				.Subtract(Elapsed)
				.Add(OneSecond);

		public bool IsOver
			=> Remaining.Ticks < 0;

		public void Dispose()
		{
			stopwatch.Reset();
		}
	}
}
