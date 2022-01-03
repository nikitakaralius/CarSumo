using DataModel.Vehicles;
using UnityEngine;

namespace Menu.Cards
{
	public interface ICardViewRepository
	{
		GameObject ViewOf(Vehicle vehicle);
	}
}