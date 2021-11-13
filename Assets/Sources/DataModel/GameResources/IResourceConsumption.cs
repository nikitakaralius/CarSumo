using System;

namespace CarSumo.DataModel.GameResources
{
	public interface IResourceConsumption
	{
		bool ConsumeIfEnoughToEnterGame();
		IObservable<bool> ObserveEnterGameConsumption();
	}
}