using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture(TestOf = typeof(Event))]
	public class EventTest
	{
		[Test]
		public void InvokeTest()
		{
			using (var @event = new Event())
			{
				var flag = false;

				@event.Add(() => flag = true);

				Assert.IsFalse(flag);

				@event.Invoke();

				Assert.IsTrue(flag);
			}
		}

		[Test]
		public void DeallocationTest()
		{
			var @event = new Event();

			var counter = 0;

			@event.Add(() => counter++);

			Assert.AreEqual(0, counter);

			@event.Invoke();

			Assert.AreEqual(1, counter);

			@event.Dispose();

			@event.Invoke();

			Assert.AreNotEqual(2, counter);
		}
	}
}