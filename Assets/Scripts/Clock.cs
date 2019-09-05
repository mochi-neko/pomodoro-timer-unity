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
		private const string timeFormat = "HH:mm:ss";

		private void Update()
		{
			var now = System.DateTime.Now;

			date.text = now.ToString(dateFormat);
			time.text = now.ToString(timeFormat);
		}

	}
}
