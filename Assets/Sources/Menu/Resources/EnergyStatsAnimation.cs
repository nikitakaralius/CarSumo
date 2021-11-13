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

		private readonly CompositeDisposable _disposables = new CompositeDisposable(1);
		
		private IResourceStorage _resourceStorage;
		private Sequence _sequence;
		
		[Inject]
		private void Construct(ResourceTimers timers, IResourceStorage storage)
		{
			_resourceStorage = storage;
			
			timers
				.TimerOf(Resource)
				.Cycles()
				.Subscribe(ConfigureVisibility)
				.AddTo(_disposables);
		}

		private void OnEnable() => _sequence = DOTween.Sequence();

		private void OnDisable()
		{
			_disposables.Dispose();
			_sequence.Kill();
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
		private void Apply(bool hide) =>
			_sequence.Append(
				(transform as RectTransform)
				.DOAnchorPos(hide
					? _positioning.To
					: _positioning.From, _positioning.Duration)
				.SetEase(_positioning.Ease));
	}
}