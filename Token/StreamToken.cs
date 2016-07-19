namespace Firebase.Xamarin.Token
{
	using System;
	using System.Reactive.Linq;
	using Firebase.Xamarin.Database.Streaming;

	public class StreamToken<T> : IDisposable
	{
		private IDisposable _observableDisposable;
		private IObservable<FirebaseEvent<T>> _observable;

		public StreamToken(IObservable<FirebaseEvent<T>> observable)
		{
			_observable = observable;
		}

		public StreamToken<T> Where(Func<FirebaseEvent<T>, bool> predicate)
		{
			_observable = _observable.Where(predicate);
			return this;
		}

		public StreamToken<T> Throttle(TimeSpan timeSpan)
		{
			_observable = _observable.Throttle(timeSpan);
			return this;
		}

		public void Subscribe(Action<FirebaseEvent<T>> onDataAdded)
			=> _observableDisposable = _observable.Subscribe(d => onDataAdded(d));

		public void Dispose()
		{
			_observableDisposable?.Dispose();
		}
	}
}