using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Menu.Deck
{
	public class MenuVehicleDeckComponent : SerializedMonoBehaviour
	{
		[SerializeField] private IPlacement _placement;
		
		private MenuVehicleDeck _menuDeck;

		[Inject]
		private void Construct(IVehicleDeck deck, IVehicleDeckOperations operations, ICardRepository repository)
		{
			_menuDeck = new MenuVehicleDeck(_placement, deck, operations, repository);
		}
		
		private void Start()
		{
			_menuDeck.Initialize();
		}

		public void ReplaceWith(ICard card, int position)
		{
			_menuDeck.ReplaceWith(card, position);
		}
	}
}