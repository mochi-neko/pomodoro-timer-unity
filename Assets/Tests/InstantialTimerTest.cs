using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture(TestOf = typeof(InstantialTimer))]
	public class InstantialTimerTest
	{
		[TestCase(3)]
		[TestCase(2)]
		[TestCase(1)]
		public async void TimeOverTest(int milliseconds)
		{
			var span = new System.TimeSpan(
				days: 0, hours: 0, minutes: 0, seconds: 0,
				milliseconds: milliseconds);

			using (var timer = new InstantialTimer(span))
			{
				Assert.IsFalse(timer.IsOver);

				await System.Threading.Tasks.Task.Delay(span);

				Assert.IsTrue(timer.IsOver);
			}
		}

		[Test]
		public void InvalidSpanTest()
		{
			Assert.DoesNotThrow(() =>
			{
				new InstantialTimer(new System.TimeSpan(1));
			});

			Assert.Throws<System.ArgumentOutOfRangeException>(() =>
			{
				new InstantialTimer(new System.TimeSpan(0));
			});

			Assert.Throws<System.ArgumentOutOfRangeException>(() =>
			{
				new InstantialTimer(new System.TimeSpan(-1));
			});
		}
	}
}
