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

		private Timer timer;
		private bool isTasking;
		public bool IsTasking => isTasking;// for test
		//private int taskCount;

		//private readonly int longRestInterval;

		public Event OnBeginTask { get; } = new Event();
		public Event OnBeginRest { get; } = new Event();
		//public Event OnBeginLongRest { get; } = new Event();

		public PomodoroTimer(TimeSpan taskSpan, TimeSpan restSpan)//, TimeSpan longRestSpan, int longRestInterval = 3)
		{
			this.taskSpan = taskSpan;
			this.restSpan = restSpan;
			//this.longRestSpan = longRestSpan;
			//this.longRestInterval = longRestInterval;

			isTasking = true;
			//taskCount = 1;
			timer = new Timer(taskSpan);
		}

		public TimeSpan RemainingRoundedUp
			=> timer.RemainingRoundedUp;

		private void BeginNewSpan()
		{
			if (timer != null)
			{
				timer.Dispose();
			}

			isTasking = !isTasking;

			if (isTasking)
			{
				timer = new Timer(taskSpan);

				//taskCount++;
				OnBeginTask?.Invoke();
			}
			//else if (taskCount >= longRestInterval)
			//{
			//	timer = new Timer(longRestSpan);

			//	OnBeginLongRest?.Invoke();
			//}
			else
			{
				timer = new Timer(restSpan);

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

			//taskCount = 0;
		}
	}
}
