using System.Collections;
using System.Collections.Generic;
using System;

namespace Mochineko.Pomodoro
{
	public class PomodoroTimer : IDisposable
	{
		private readonly TimeSpan taskSpan;
		private readonly TimeSpan restSpan;
		//private readonly TimeSpan longRestSpan;

		private InstantialTimer timer;

		public bool IsTasking { get; private set; }
		public int TaskCount { get; private set; }

		//private readonly int longRestInterval;

		public ManagedEvent OnBeginTask { get; } = new ManagedEvent();
		public ManagedEvent OnBeginRest { get; } = new ManagedEvent();
		//public Event OnBeginLongRest { get; } = new Event();

		public PomodoroTimer(TimeSpan taskSpan, TimeSpan restSpan)//, TimeSpan longRestSpan, int longRestInterval = 3)
		{
			this.taskSpan = taskSpan;
			this.restSpan = restSpan;
			//this.longRestSpan = longRestSpan;
			//this.longRestInterval = longRestInterval;

			IsTasking = true;
			TaskCount = 1;
			timer = new InstantialTimer(taskSpan);
		}

		public TimeSpan RemainingRoundedUp
			=> timer.RemainingSecoundsRoundedUp;

		private void BeginNewSpan()
		{
			if (timer != null)
			{
				timer.Dispose();
			}

			IsTasking = !IsTasking;

			if (IsTasking)
			{
				timer = new InstantialTimer(taskSpan);

				TaskCount++;
				OnBeginTask?.Invoke();
			}
			//else if (taskCount >= longRestInterval)
			//{
			//	timer = new Timer(longRestSpan);

			//	OnBeginLongRest?.Invoke();
			//}
			else
			{
				timer = new InstantialTimer(restSpan);

				OnBeginRest?.Invoke();
			}
		}
		
		public void Update()
		{
			if (timer == null)
			{
				return;
			}
			if (!timer.IsOver)
			{
				return;
			}
			
			BeginNewSpan();
		}

		public void Dispose()
		{
			if (timer == null)
			{
				return;
			}

			timer.Dispose();
			timer = null;

			TaskCount = 0;
		}
	}
}
