using Sirenix.OdinInspector;
using UnityEngine;

namespace Sources.UI.Endgame
{
	public class EndgameWindow : MonoBehaviour
	{
		[SerializeField, Required, SceneObjectsOnly] private GameObject _gameplayElements;
		
		public void Show()
		{
			_gameplayElements.SetActive(false);
			gameObject.SetActive(true);
		}
	}
}