using DataModel.Vehicles;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Deck
{
	public class CardInStorage : MonoBehaviour, ICard, IPointerClickHandler
	{
		private IDeckSelection _deckSelection;
		
		public CardInStorage Initialize(VehicleId vehicleId, IDeckSelection deckSelection)
		{
			VehicleId = vehicleId;
			_deckSelection = deckSelection;
			return this;
		}

		public VehicleId VehicleId { get; private set; }
		
		public void OnPointerClick(PointerEventData eventData)
		{
			_deckSelection.Select(this);
		}
	}
}