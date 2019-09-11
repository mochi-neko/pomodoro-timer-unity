using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture]
	public class SystemTimerTest
	{
		[TestCase(3)]
		[TestCase(2)]
		[TestCase(1)]
		public async void IntervalTest(int interval)
		{
			using (var timer = new System.Timers.Timer(interval))
			{
				var hasBeenTimeOver = false;
				timer.Elapsed += (sender, argument) => hasBeenTimeOver = true;

				Assert.IsFalse(hasBeenTimeOver);

				timer.Start();
				await Task.Delay(interval);

				Assert.IsTrue(hasBeenTimeOver);

				timer.Stop();
			}
		}
	}
}