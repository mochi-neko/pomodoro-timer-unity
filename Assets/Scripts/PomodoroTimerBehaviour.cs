using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Mochineko.Pomodoro
{
	public class PomodoroTimerBehaviour : MonoBehaviour
	{
		[SerializeField]
		private Text timeText;

		private PomodoroTimer timer;

		private const string timeFormat = @"hh\:mm\:ss";

		public void StartPomodoro()
		{
			if (timer != null)
			{
				timer.Dispose();
			}

			timer = new PomodoroTimer(
				new TimeSpan(hours: 0, minutes: 0, seconds: 10),
				new TimeSpan(hours: 0, minutes: 0, seconds: 5)
			);
		}

		public void StopPomodoro()
		{
			if (timer == null)
			{
				return;
			}

			timer.Dispose();
			timer = null;
		}

		private void Update()
		{
			if (timer == null)
			{
				return;
			}

			timeText.text = timer.RemainingRoundedUp.ToString(timeFormat);
		}
	}
}
