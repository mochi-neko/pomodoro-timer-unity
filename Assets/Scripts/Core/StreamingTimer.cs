using System.Collections;
using System.Collections.Generic;
using System;

namespace Mochineko.Pomodoro
{
	public class StreamingTimer : IDisposable
	{
		public ManagedEvent OnStart { get; } = new ManagedEvent();
		public ManagedEvent<string> OnProcess = new ManagedEvent<string>();
		public ManagedEvent OnElapsed { get; } = new ManagedEvent();
		public ManagedEvent OnReset { get; } = new ManagedEvent();

		private InstantialTimer timer;
		private readonly TimeSpan span;
		private const string format = @"mm\:ss";

		public StreamingTimer(TimeSpan span)
		{
			if (span.Ticks <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(span));
			}

			this.span = span;
		}

		public void Start()
		{
			if (timer != null)
			{
				DisposeTimer();
			}

			timer = new InstantialTimer(span);

			OnStart?.Invoke();
		}

		public void Process()
		{
			if (timer == null)
			{
				return;
			}

			if (!timer.IsOver)
			{
				OnProcess?.Invoke(
					timer
					.RemainingSecoundsRoundedUp
					.ToString(format)
				);
				return;
			}

			OnElapsed?.Invoke();

			DisposeTimer();
		}

		public void Reset()
		{
			if (timer == null)
			{
				return;
			}

			DisposeTimer();

			OnReset?.Invoke();
		}

		public void Dispose()
		{
			DisposeTimer();

			OnStart.Dispose();
			OnReset?.Dispose();
		}

		private void DisposeTimer()
		{
			timer.Dispose();
			timer = null;
		}
	}
}