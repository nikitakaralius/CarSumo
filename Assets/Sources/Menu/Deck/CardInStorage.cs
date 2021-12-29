using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public class CardInStorage : MonoBehaviour, ICard
	{
		public VehicleId VehicleId { get; }
	}
}