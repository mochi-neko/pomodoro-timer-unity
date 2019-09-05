using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class TimerBehaviour : MonoBehaviour
	{
		[SerializeField]
		private int spanMinutes = 1;
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

		private Timer timer = null;

		private const string timeFormat = @"hh\:mm\:ss";

		[ContextMenu(nameof(Switch))]
		public void Switch()
		{
			if (timer == null)
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
			if (timer != null)
			{
				StopTimer();
			}

			timer = new Timer(SpanMinutes);
		}

		public void StopTimer()
		{
			timer.Dispose();
			timer = null;
		}

		private void Start()
		{
			DisplaySpanMinutes();
		}

		private void Update()
		{
			if (timer == null)
			{
				return;
			}

			if (timer.IsOver)
			{
				StopTimer();
				switchText.text = "Start";
				DisplaySpanMinutes();

				return;
			}

			remaiginTime.text = timer.RemainingRoundedUp.ToString(timeFormat);
		}
	}
}
