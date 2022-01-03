using System;
using DataModel.Vehicles;
using DG.Tweening;
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
		private Tween _animation;
		
		public CardInDeck Initialize(Vehicle vehicle, int position)
		{
			Vehicle = vehicle;
			_position = position;
			return this;
		}
		
		public Vehicle Vehicle { get; private set; }

		private RectTransform RectTransform => (RectTransform) transform;

		private void OnDestroy()
		{
			_animation?.Kill(true);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			_clicked.OnNext(_position);
		}

		public IObservable<int> ObserveOnClicked() => _clicked; 

		public void PlayReadyToChangeAnimation()
		{
			_animation = RectTransform
				.DOScale(transform.localScale + Vector3.one * 0.05f, 1.0f)
				.SetEase(Ease.InOutBack)
				.SetLoops(-1, LoopType.Yoyo);
		}

		public void StopPlayingReadyToChangeAnimation()
		{
			_animation.Kill(true);
		}
	}
}