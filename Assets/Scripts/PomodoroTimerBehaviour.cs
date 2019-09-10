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
		private int taskMinutes = 25;
		[SerializeField]
		private int restMinutes = 5;
		[SerializeField]
		private int longRestMinutes = 15;
		[SerializeField]
		private Text timeText;

		public UnityEvent onBeginTask = new UnityEvent();
		public UnityEvent onBeginRest = new UnityEvent();

		public UnityEvent onStart = new UnityEvent();
		public UnityEvent onStop = new UnityEvent();

		private PomodoroTimer timer;

		private TimeSpan TaskSpan
			=> new TimeSpan(hours: 0, minutes: taskMinutes, seconds: 0);
		private TimeSpan RestSpan
			=> new TimeSpan(hours: 0, minutes: restMinutes, seconds: 0);
		private TimeSpan LongRestSpan
			=> new TimeSpan(hours: 0, minutes: longRestMinutes, seconds: 0);

		private const string timeFormat = @"mm\:ss";

		public void StartPomodoro()
		{
			if (timer != null)
			{
				timer.Dispose();
			}

			timer = new PomodoroTimer(TaskSpan, RestSpan);//, LongRestSpan);

			timer.OnBeginTask.Add(InvokeOnBeginTask);
			timer.OnBeginRest.Add(InvokeOnBeginRest);

			InvokeOnBeginTask();

			onStart?.Invoke();
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

			onStop?.Invoke();
		}

		private void DisplayTaskTimeText()
		{
			timeText.text = TaskSpan.ToString(timeFormat);
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
