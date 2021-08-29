using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using TweenAnimations;
using UnityEngine;

namespace Shop.ExceptionMessaging
{
	[RequireComponent(typeof(RectTransform))]
	public class ExceptionMessage : MonoBehaviour, IExceptionMessage
	{
		[Header("Message Preferences")]
		[SerializeField] private TMP_Text _messageText;
		[MinValue(0.0f)] [SerializeField] private float _timePerMessage;

		[Header("Animation Preferences")] 
		[SerializeField] private Vector2TweenData _positionTweenData;

		private RectTransform _rectTransform;
		private Tween _animation;
		
		private void Start()
		{
			_rectTransform = (RectTransform) transform;
			RestoreOriginPosition();
		}

		private void OnDestroy()
		{
			RestoreOriginPosition();
			_animation?.Kill();
		}

		public void Show(string message)
		{
			_messageText.text = message;
			PlayAnimation();
		}

		private void PlayAnimation() =>
			_animation = _rectTransform
				.DOAnchorPos(_positionTweenData.To, _positionTweenData.Duration)
				.SetEase(_positionTweenData.Ease)
				.OnComplete(() =>
				{
					_rectTransform
						.DOAnchorPos(_positionTweenData.From, _positionTweenData.Duration)
						.SetEase(_positionTweenData.Ease)
						.SetDelay(_timePerMessage);
				});

		private void RestoreOriginPosition() =>
			_rectTransform.anchoredPosition = _positionTweenData.From;
	}
}