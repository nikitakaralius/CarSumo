using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public class CardInDeck : MonoBehaviour, ICard
	{
		public VehicleId VehicleId { get; private set; }

		public CardInDeck Initialize(VehicleId id)
		{
			VehicleId = id;
			return this;
		}
	}
}