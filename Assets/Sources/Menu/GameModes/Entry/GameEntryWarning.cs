using CarSumo.DataModel.GameResources;
using UniRx;
using UnityEngine;
using Zenject;

namespace Sources.Menu.GameModes.Entry
{
	public class GameEntryWarning : MonoBehaviour
	{
		[Header("View"), SerializeField] private GameObject _popup;

		private readonly CompositeDisposable _disposables = new CompositeDisposable();
		
		[Inject]
		private void Construct(IResourceConsumption consumption) =>
			consumption
				.ObserveEnterGameConsumption()
				.Subscribe(availableToEnter => _popup.SetActive(availableToEnter == false))
				.AddTo(_disposables);

		private void OnDestroy() => _disposables.Dispose();
	}
}