using System;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;

namespace Menu.Cards
{
	public class CardInDeck : SerializedMonoBehaviour, ICard, IPointerClickHandler
	{
		private int _position;
		
		public CardInDeck Initialize(Vehicle vehicle, int position)
		{
			Vehicle = vehicle;
			_position = position;
			return this;
		}

		public event Action<int> Clicked; 

		public Vehicle Vehicle { get; private set; }

		public void OnPointerClick(PointerEventData eventData)
		{
			Clicked?.Invoke(_position);
		}

		public void PlayReadyToChangeAnimation()
		{
			throw new NotImplementedException();
		}
	}
}