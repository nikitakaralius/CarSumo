using System;
using UniRx;
using UnityEngine;

namespace DataModel.GameData.Infrastructure
{
	public class ApplicationEvents : MonoBehaviour, IApplicationEvents
	{
		private readonly Subject<bool> _quit = new Subject<bool>();
		private readonly Subject<bool> _paused = new Subject<bool>();

		public IObservable<bool> ObserveQuit() => _quit;

		public IObservable<bool> ObservePaused() => _paused;

		private void OnApplicationQuit() => _quit.OnNext(true);

		private void OnApplicationPause(bool pauseStatus) => _paused.OnNext(pauseStatus);
	}
}