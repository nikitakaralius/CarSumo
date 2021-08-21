using System.Linq;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;
using Zenject;

namespace Menu.Tabs
{
	public class Tab : MonoBehaviour
	{
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();

		[Inject]
		private void Construct()
		{
			TabElement[] elements = GetComponentsInChildren<TabElement>();
			
			foreach (TabElement tabElement in elements)
			{
				tabElement
					.ObserveSelected()
					.Subscribe(element => elements
						.Where(x => x != element)
						.ForEach(x => x.SetSelected(false)))
					.AddTo(_subscriptions);
			}
		}
		
		private void OnDestroy() => _subscriptions?.Dispose();
	}
}