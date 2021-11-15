using Game.Endgame;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI.Endgame
{
	public class EndgameWindowsRoot : MonoBehaviour, IEndGameStatusVisitor
	{
		[Header("Other")]
		[SerializeField, Required, SceneObjectsOnly] private GameObject _gameplayElements;
		
		[Header("Endgame")]
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _singleModeWin;
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _singleModeLose;
		[SerializeField, Required, SceneObjectsOnly] private EndgameWindow _oneDeviceModeEndGame;
		
		public void Visit(SingleModeWin status) => Show(_singleModeWin);

		public void Visit(SingleModeLose status) => Show(_singleModeLose);

		public void Visit(OneDeviceEndGame status) => Show(_oneDeviceModeEndGame);

		private void Show(EndgameWindow window)
		{
			_gameplayElements.SetActive(false);
			window.Show();
		}
	}
}