using System;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Resources
{
	public class ResourceTimeView : MonoBehaviour
	{
		[Header("Configuration"), SerializeField] private TimedResource _resource;
		[Header("View"), SerializeField] private TextMeshProUGUI _timer;

		[Inject]
		private void Construct(ResourceTimers timers) =>
			timers
				.TimerOf(_resource)
				.TimeLeft()
				.Subscribe(ChangeTimerText);

		private void ChangeTimerText(TimeSpan timeSpan) => _timer.text = timeSpan.ToString("mm\\:ss");
	}
}