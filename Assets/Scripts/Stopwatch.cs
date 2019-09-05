using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class Stopwatch : MonoBehaviour
	{
		[SerializeField]
		private Text elapsedTime;

		[SerializeField]
		private Text switchText;
		private void DisplayStartText()
		{
			switchText.text = "Start";
		}
		private void DisplayStopText()
		{
			switchText.text = "Stop";
		}

		private System.Diagnostics.Stopwatch stopwatch
			= new System.Diagnostics.Stopwatch();

		private const string timeFormat = @"hh\:mm\:ss";

		[ContextMenu(nameof(SwitchStartAndStop))]
		public void SwitchStartAndStop()
		{
			if (!stopwatch.IsRunning)
			{
				StartTime();

				DisplayStopText();
			}
			else
			{
				StopTime();

				DisplayStartText();
			}
		}

		[ContextMenu("Start")]
		public void StartTime()
		{
			stopwatch.Start();
		}

		[ContextMenu("Stop")]
		public void StopTime()
		{
			stopwatch.Stop();
		}

		[ContextMenu("Reset")]
		public void ResetTime()
		{
			stopwatch.Reset();

			DisplayStartText();
		}

		[ContextMenu("Restart")]
		public void RestartTime()
		{
			stopwatch.Restart();
		}

		private void Start()
		{
			elapsedTime.text = "00:00:00";
		}

		private void Update()
		{
			elapsedTime.text = stopwatch.Elapsed.ToString(timeFormat);
		}
	}
}