using DataModel.Vehicles;
using UnityEngine.AddressableAssets;

namespace Menu.Deck
{
	public interface ICardRepository
	{
		AssetReferenceGameObject ViewOf(VehicleId id);
	}
}