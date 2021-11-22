using System;
using CarSumo.DataModel.GameResources;
using DG.Tweening;
using Sirenix.OdinInspector;
using TweenAnimations;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Resources
{
	public class EnergyStatsAnimation : MonoBehaviour
	{
		private const ResourceId Id = ResourceId.Energy;
		private const TimedResource Resource = TimedResource.GameEnergy;

		[SerializeField] private TweenData<Vector2> _positioning;

		private readonly CompositeDisposable _disposables = new CompositeDisposable();

		private ResourceTimers _timers;
		private IResourceStorage _resourceStorage;
		private Tween _animation;
		
		[Inject]
		private void Construct(ResourceTimers timers, IResourceStorage storage)
		{
			_resourceStorage = storage;
			_timers = timers;
		}

		private void OnEnable() =>
			_timers
				.TimerOf(Resource)
				.Cycles()
				.Subscribe(ConfigureVisibility)
				.AddTo(_disposables);

		private void OnDisable()
		{
			_animation?.Kill();
			_disposables.Dispose();
		}

		private void ConfigureVisibility(int cycles)
		{
			int amount = _resourceStorage.GetResourceAmount(Id).Value;
			int? limit = _resourceStorage.GetResourceLimit(Id).Value;

			if (limit.HasValue == false)
				throw new InvalidOperationException($"{Id} limit must be specified");

			Apply(amount + cycles >= limit.Value);
		}


		[Button(Style = ButtonStyle.FoldoutButton), DisableInEditorMode]
		private void Apply(bool hide)
		{
			_animation?.Kill();
			
			_animation = (transform as RectTransform)
				.DOAnchorPos(hide
					? _positioning.To
					: _positioning.From, _positioning.Duration)
				.SetEase(_positioning.Ease);
		}
	}
}