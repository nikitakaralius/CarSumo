using System;
using DataModel.Vehicles;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Menu.Cards
{
	public class CardInDeck : SerializedMonoBehaviour, ICard, IPointerClickHandler
	{
		private readonly Subject<int> _clicked = new Subject<int>();
		private int _position;
		
		public CardInDeck Initialize(Vehicle vehicle, int position)
		{
			Vehicle = vehicle;
			_position = position;
			return this;
		}
		
		public Vehicle Vehicle { get; private set; }

		public void OnPointerClick(PointerEventData eventData)
		{
			_clicked.OnNext(_position);
		}

		public IObservable<int> OnClicked() => _clicked; 

		public void PlayReadyToChangeAnimation()
		{
			Debug.LogError("Animation is not set up");
		}

		public void StopPlayingReadyToChangeAnimation()
		{
			Debug.LogError("Animation is not set up");
		}
	}
}