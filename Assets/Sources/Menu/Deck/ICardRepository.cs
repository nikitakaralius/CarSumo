using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Deck
{
	public interface ICardRepository
	{
		GameObject ViewBy(VehicleId id);
	}
}