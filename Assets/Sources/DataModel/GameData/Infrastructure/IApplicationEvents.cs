using System;

namespace DataModel.GameData.Infrastructure
{
	public interface IApplicationEvents
	{
		IObservable<bool> ObserveQuit();
		IObservable<bool> ObservePaused();
	}
}