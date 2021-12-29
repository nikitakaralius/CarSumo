using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public class CardInDeck : MonoBehaviour, ICard
	{
		public VehicleId VehicleId { get; }
	}
}