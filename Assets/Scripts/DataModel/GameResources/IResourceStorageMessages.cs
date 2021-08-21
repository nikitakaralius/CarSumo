using System;

namespace CarSumo.DataModel.GameResources
{
	public interface IResourceStorageMessages
	{
		IObservable<ResourceId> ObserveResourceChanged();
	}
}