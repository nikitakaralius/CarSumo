using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public class CardInDeck : MonoBehaviour, ICard
	{
		public CardInDeck Initialize(VehicleId id)
		{
			VehicleId = id;
			return this;
		}

		public VehicleId VehicleId { get; private set; }
	}
}