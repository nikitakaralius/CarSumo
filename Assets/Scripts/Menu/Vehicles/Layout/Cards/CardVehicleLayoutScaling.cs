using DG.Tweening;
using TweenAnimations;
using UnityEngine;

namespace Menu.Vehicles.Layout
{
	public class CardVehicleLayoutScaling : MonoBehaviour
	{
		[SerializeField] private Vector3TweenData _sizeData;

		private Tween _selectedSizeTween;
		private Tween _deselectedSizeTween;

		private void OnDisable()
		{
			_selectedSizeTween?.Kill();
			_deselectedSizeTween?.Kill();
		}

		public void ApplySelectedAnimation(Transform card)
		{
			_selectedSizeTween?.Kill(true);
			
			_selectedSizeTween = card.DOScale(_sizeData.To, _sizeData.Duration)
				.SetEase(_sizeData.Ease);
		}

		public void ApplyDeselectedAnimation(Transform card)
		{
			_deselectedSizeTween?.Kill(true);
			
			_deselectedSizeTween = card.DOScale(_sizeData.From, _sizeData.Duration)
				.SetEase(_sizeData.Ease);
		}

		public void ApplyInitialScale(Transform card)
		{
			card.localScale = _sizeData.From;
		}

		public void ApplySelectedScale(Transform card)
		{
			card.localPosition = _sizeData.To;
		}
	}
}