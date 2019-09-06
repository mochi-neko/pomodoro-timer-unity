using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mochineko.Pomodoro
{
	public class TextColorChanger : MonoBehaviour
	{
		[SerializeField]
		private Text target;

		[SerializeField]
		private Color defaultColor;
		public void Default()
		{
			target.color = defaultColor;
		}

		[SerializeField]
		private Color deactiveColor;
		public void Activate()
		{
			target.color = activeColor;
		}

		[SerializeField]
		private Color activeColor;
		public void Deactivate()
		{
			target.color = deactiveColor;
		}
	}
}