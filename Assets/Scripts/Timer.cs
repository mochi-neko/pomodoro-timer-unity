using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class Timer : MonoBehaviour
	{
		[SerializeField]
		private int spanMinutes = 5;
		private System.TimeSpan SpanMinutes
			=> new System.TimeSpan(
				hours: 0,
				minutes: spanMinutes,
				seconds: 0
			);
		[SerializeField]
		private Text remaiginTime;
		private void DisplaySpanMinutes()
			=> remaiginTime.text = SpanMinutes.ToString(timeFormat);
		[SerializeField]
		private Text switchText;

		private System.Diagnostics.Stopwatch stopwatch 
			= new System.Diagnostics.Stopwatch();

		private const string timeFormat = @"hh\:mm\:ss";

		[ContextMenu(nameof(Switch))]
		public void Switch()
		{
			if (!stopwatch.IsRunning)
			{
				StartTimer();

				switchText.text = "Stop";
			}
			else
			{
				StopTimer();

				switchText.text = "Start";
				DisplaySpanMinutes();
			}
		}

		public void StartTimer()
		{
			stopwatch.Restart();
		}

		public void StopTimer()
		{
			stopwatch.Reset();
		}

		private void Start()
		{
			DisplaySpanMinutes();
		}

		private void Update()
		{
			if (!stopwatch.IsRunning)
			{
				return;
			}

			var remaining = 
				SpanMinutes
				.Subtract(stopwatch.Elapsed);

			if (remaining.Ticks <= 0)
			{
				remaiginTime.text = "Finished";
				return;
			}

			remaiginTime.text = 
				remaining
				.Add(new System.TimeSpan(hours: 0, minutes: 0, seconds: 1))
				.ToString(timeFormat);
		}
	}
}
