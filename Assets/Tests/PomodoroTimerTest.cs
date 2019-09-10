using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture(TestOf = typeof(PomodoroTimer))]
	public class PomodoroTimerTest
	{
		[TestCase(1, 2)]
		[TestCase(2, 1)]
		[TestCase(1, 1)]
		public async void ChangeStateTest(int taskTicks, int restTicks)
		{
			var taskSpan = new System.TimeSpan(taskTicks);
			var restSpan = new System.TimeSpan(restTicks);

			using (var timer = new PomodoroTimer(
				taskSpan: taskSpan,
				restSpan: restSpan
				))
			{
				Assert.IsTrue(timer.IsTasking);

				await System.Threading.Tasks.Task.Delay(taskSpan);

				timer.Update();
				Assert.IsFalse(timer.IsTasking);

				await System.Threading.Tasks.Task.Delay(restSpan);

				timer.Update();
				Assert.IsTrue(timer.IsTasking);
			}
		}
	}
}