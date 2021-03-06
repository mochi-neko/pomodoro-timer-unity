﻿using System.Collections;
using System.Collections.Generic;
using System;

namespace Mochineko.Pomodoro
{
	public class PomodoroTimer : IDisposable
	{
		private readonly TimeSpan taskSpan;
		private readonly TimeSpan restSpan;

		private Timer timer;
		private bool isTasking;

		public Event OnBeginTask { get; } = new Event();
		public Event OnBeginRest { get; } = new Event();

		public PomodoroTimer(TimeSpan taskSpan, TimeSpan restSpan)
		{
			this.taskSpan = taskSpan;
			this.restSpan = restSpan;

			isTasking = true;
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

				OnBeginTask?.Invoke();
			}
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
		}
	}
}
