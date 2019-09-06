using System.Collections.Generic;
using System;

namespace Mochineko.Pomodoro
{
	public class Event : IDisposable
	{
		private HashSet<Action> caches = new HashSet<Action>();

		public void Add(Action action)
		{
			caches.Add(action);
		}

		public void Remove(Action action)
		{
			caches.Remove(action);
		}

		public void Invoke()
		{
			foreach (var cache in caches)
			{
				cache?.Invoke();
			}
		}

		public void Dispose()
		{
			caches.Clear();
		}
	}

	public class Event<T> : IDisposable
	{
		private HashSet<Action<T>> caches = new HashSet<Action<T>>();

		public void Add(Action<T> action)
		{
			caches.Add(action);
		}

		public void Remove(Action<T> action)
		{
			caches.Remove(action);
		}

		public void Invoke(T value)
		{
			foreach (var cache in caches)
			{
				cache?.Invoke(value);
			}
		}

		public void Dispose()
		{
			caches.Clear();
		}
	}
}
