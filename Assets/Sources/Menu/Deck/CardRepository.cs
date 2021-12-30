using DataModel.GameData.Vehicles;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Menu.Deck
{
	[CreateAssetMenu(menuName = "Cards/Repository", fileName = "CardRepository")]
	public class CardRepository : SerializedScriptableObject, ICardRepository
	{
		[SerializeField] private IVehicleAssets _assets;
		
		public AssetReferenceGameObject ViewOf(VehicleId id)
		{
			return _assets.GetAssetByVehicleId(id);
		}
	}
}