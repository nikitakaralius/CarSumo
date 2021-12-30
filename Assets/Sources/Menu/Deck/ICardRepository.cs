using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public interface ICardRepository
	{
		GameObject ViewOf(VehicleId id);
	}
}