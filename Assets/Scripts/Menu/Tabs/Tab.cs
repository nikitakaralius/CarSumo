using System.Linq;
using Sirenix.Utilities;
using UniRx;
using UnityEngine;

namespace Menu.Tabs
{
	public class Tab : MonoBehaviour
	{
		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		private void Start()
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