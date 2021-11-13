using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Resources
{
	public class TimedResourceView : MonoBehaviour
	{
		[Header("View"), SerializeField] private TextMeshProUGUI _timer;
		[Header("Preferences"), SerializeField] private TimedResource _resource;
		
		private readonly CompositeDisposable _disposables = new CompositeDisposable(1);
		
		[Inject]
		private void Construct(ResourceTimers timers) =>
			timers
				.TimerOf(_resource)
				.TimeLeft()
				.Subscribe(ChangeTimerText)
				.AddTo(_disposables);

		private void OnDisable() => _disposables.Dispose();

		private void ChangeTimerText(TimeSpan timeSpan) => _timer.text = timeSpan.ToString("mm\\:ss");
	}
}