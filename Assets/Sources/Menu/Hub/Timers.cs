using Menu.Resources;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Menu.Hub
{
	public class Timers : SerializedMonoBehaviour
	{
		private ResourceTimers _timers;

		[Inject]
		private void Construct(ResourceTimers timers)
		{
			_timers = timers;
		}

		[Button(Style = ButtonStyle.FoldoutButton), DisableInEditorMode]
		private void LogResourceTimeLeft(TimedResource resource) => Debug.Log(_timers.TimerOf(resource).TimeLeft);
	}
}