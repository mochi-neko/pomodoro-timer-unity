using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class Clock : MonoBehaviour
	{
		[SerializeField]
		private Text date;
		[SerializeField]
		private Text time;

		private const string dateFormat = "yyyy/MM/dd";
		private const string timeFormat = "HH:mm";

		private void Update()
		{
			var now = System.DateTime.Now;

			if (date != null)
			{
				date.text = now.ToString(dateFormat);
			}

			if (time != null)
			{
				time.text = now.ToString(timeFormat);
			}
		}

	}
}
