using Game;
using GuiBaseData.Accounts;
using Sirenix.OdinInspector;
using Game.Endgame;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Endgame
{
	public class WinnerView : MonoBehaviour
	{
		[SerializeField, Required, SceneObjectsOnly] private AccountView _view;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable(1);
		
		[Inject]
		private void Construct(IEndGameMessage message) =>
			message
				.ObserveEnding()
				.Subscribe(_view.ChangeAccount)
				.AddTo(_subscriptions);

		private void OnDestroy() => _subscriptions.Dispose();
	}
}