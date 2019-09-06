using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

namespace Mochineko.Pomodoro
{
	public class PomodoroTimerBehaviour : MonoBehaviour
	{
		[SerializeField]
		private Text timeText;

		public UnityEvent onBeginTask = new UnityEvent();
		public UnityEvent onBeginRest = new UnityEvent();

		private PomodoroTimer timer;

		private readonly TimeSpan taskSpan 
			= new TimeSpan(hours: 0, minutes: 0, seconds: 10);
		private readonly TimeSpan restSpan
			= new TimeSpan(hours: 0, minutes: 0, seconds: 5);

		private const string timeFormat = @"hh\:mm\:ss";

		public void StartPomodoro()
		{
			if (timer != null)
			{
				timer.Dispose();
			}

			timer = new PomodoroTimer(taskSpan, restSpan);

			timer.OnBeginTask.Add(InvokeOnBeginTask);
			timer.OnBeginRest.Add(InvokeOnBeginRest);

			InvokeOnBeginTask();
		}

		private void InvokeOnBeginTask()
			=> onBeginTask?.Invoke();

		private void InvokeOnBeginRest()
			=> onBeginRest?.Invoke();

		public void StopPomodoro()
		{
			if (timer == null)
			{
				return;
			}

			timer.Dispose();
			timer = null;

			DisplayTaskTimeText();
		}

		private void DisplayTaskTimeText()
		{
			timeText.text = taskSpan.ToString(timeFormat);
		}

		private void Start()
		{
			DisplayTaskTimeText();
		}

		private void Update()
		{
			if (timer == null)
			{
				return;
			}

			timer.Update();

			timeText.text = timer.RemainingRoundedUp.ToString(timeFormat);
		}
	}
}
