using System;

namespace Mochineko.Pomodoro
{
	public class Timer : IDisposable
	{
		private readonly TimeSpan duration;
		private readonly System.Diagnostics.Stopwatch stopwatch;

		private readonly TimeSpan OneSecond
			= new TimeSpan(hours: 0, minutes: 0, seconds: 1);

		public Timer(TimeSpan duration)
		{
			this.duration = duration;
			this.stopwatch = new System.Diagnostics.Stopwatch();

			stopwatch.Start();
		}

		public TimeSpan Elapsed
			=> stopwatch.Elapsed;

		public TimeSpan Remaining
			=> duration.Subtract(Elapsed);

		public TimeSpan RemainingRoundedUp
			=> duration
				.Subtract(Elapsed)
				.Add(OneSecond);

		public bool IsOver
			=> Remaining.TotalMilliseconds <= 0;

		public void Dispose()
		{
			stopwatch.Stop();
		}
	}
}
