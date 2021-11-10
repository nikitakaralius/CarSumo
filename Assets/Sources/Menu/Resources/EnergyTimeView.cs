using System;
using CarSumo.DataModel.GameResources;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using TweenAnimations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Resources
{
	public class EnergyTimeView : MonoBehaviour
	{
		private const ResourceId Id = ResourceId.Energy;
		private const TimedResource Resource = TimedResource.GameEnergy;
		
		[Header("View"), SerializeField] private TextMeshProUGUI _timer;
		[Header("Animation"), SerializeField] private TweenData<Vector2> _positioningAnimation;

		private IResourceStorage _resourceStorage;
		private Tween _animation;
		
		[Inject]
		private void Construct(ResourceTimers timers, IResourceStorage storage)
		{
			_resourceStorage = storage;
			
			timers
				.TimerOf(Resource)
				.TimeLeft()
				.Subscribe(ChangeTimerText);

			timers
				.TimerOf(Resource)
				.Cycles()
				.Subscribe(ConfigureVisibility);
		}

		private void OnDisable() => _animation?.Kill();

		private void ChangeTimerText(TimeSpan timeSpan) => _timer.text = timeSpan.ToString("mm\\:ss");

		private void ConfigureVisibility(int cycles)
		{
			int amount = _resourceStorage.GetResourceAmount(Id).Value;
			int? limit = _resourceStorage.GetResourceLimit(Id).Value;

			if (limit.HasValue == false)
				throw new InvalidOperationException($"{Id} limit must be specified");

			ApplyAnimation(amount + cycles >= limit.Value);
		}

		[Button, DisableInEditorMode]
		private void ApplyAnimation(bool hide)
		{
			_animation?.Kill();

			_animation = (transform as RectTransform)
				.DOAnchorPos(hide
					? _positioningAnimation.To
					: _positioningAnimation.From, _positioningAnimation.Duration)
				.SetEase(_positioningAnimation.Ease);
		}
	}
}