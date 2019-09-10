using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

		public UnityEvent onStarted = new UnityEvent();
		public UnityEvent onFinished = new UnityEvent();
		public UnityEvent onStopped = new UnityEvent();

		private Timer timer = null;

		private const string timeFormat = @"mm\:ss";

		public void Append(int minutes)
		{
			spanMinutes += minutes;

			if (spanMinutes < 1)
			{
				spanMinutes = 1;
			}
			else if (spanMinutes > 60)
			{
				spanMinutes = 60;
			}

			DisplaySpanMinutes();
		}

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

			onStarted?.Invoke();
		}

		public void StopTimer()
		{
			timer.Dispose();
			timer = null;

			onStopped?.Invoke();
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
				onFinished?.Invoke();
				StopTimer();
				switchText.text = "Start";
				DisplaySpanMinutes();

				return;
			}

			remaiginTime.text = timer.RemainingRoundedUp.ToString(timeFormat);
		}
	}
}
