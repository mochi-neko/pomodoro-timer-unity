using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class Clock : MonoBehaviour
	{
		[SerializeField]
		private Text text;

		private void Update()
		{
			text.text = System.DateTime.Now.ToString();
		}

	}
}
