using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture(TestOf = typeof(StreamingTimer))]
	public class StreamingTimerTest
	{
		[Test]
		public async void ElapsTest()
		{
			var span = new TimeSpan(0, 0, 0, 0, 1);
			var timer = new StreamingTimer(span);
			var flag = false;

			timer.OnElapsed.Add(() => flag = true);

			timer.Start();

			await Task.Delay(span);

			timer.Process();

			Assert.IsTrue(flag);
		}
	}
}