using Game.Endgame;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Endgame
{
	public class EndgameWindowsRoot : MonoBehaviour
	{
		[Header("Other")]
		[SerializeField, Required, SceneObjectsOnly] private GameObject _gameplayElements;
		
		[Header("Endgame")]
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _singleModeWin;
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _singleModeLose;
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _oneDeviceModeEndGame;

		private readonly CompositeDisposable _subscriptions = new CompositeDisposable();
		
		[Inject]
		private void Construct(IEndGameMessage message) =>
			message
				.ObserveEnding()
				.Subscribe(status => status.Match(Visit, Visit, Visit))
				.AddTo(_subscriptions);

		private void OnDisable() => _subscriptions.Dispose();
		
		private void Visit(SingleModeWin status) => Show(_singleModeWin);

		private void Visit(SingleModeLose status) => Show(_singleModeLose);

		private void Visit(OneDeviceEndGame status) => Show(_oneDeviceModeEndGame);

		private void Show(EndgameWindow window)
		{
			_gameplayElements.SetActive(false);
			window.Show();
		}
	}
}