using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mochineko.Pomodoro.Tests
{
	[TestFixture(TestOf = typeof(ManagedEvent))]
	public class ManagedEventTest
	{
		[Test]
		public void InvokeTest()
		{
			using (var managedEvent = new ManagedEvent())
			{
				var flag = false;

				managedEvent.Add(() => flag = true);

				Assert.IsFalse(flag);

				managedEvent.Invoke();

				Assert.IsTrue(flag);
			}
		}

		[Test]
		public void DeallocationTest()
		{
			var managedEvent = new ManagedEvent();

			var counter = 0;

			managedEvent.Add(() => counter++);

			Assert.AreEqual(0, counter);

			managedEvent.Invoke();

			Assert.AreEqual(1, counter);

			managedEvent.Dispose();

			managedEvent.Invoke();

			Assert.AreNotEqual(2, counter);

			managedEvent.Dispose();
		}
	}
}