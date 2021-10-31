using System;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.Accounts.AccountEditor
{
	public class RemoveAccountButton : MonoBehaviour
	{
		[Header("Required Components")]
		[SerializeField] private Button _button;
		[SerializeField] private GameObject _window;
		[SerializeField] private GameObject _warningWindow;

		private Func<bool> _onButtonClickedHandler;
		
		private void Start()
		{
			_button.onClick.AddListener(() =>
			{
				if (_onButtonClickedHandler is null)
					return;
				
				bool operationSuccess = _onButtonClickedHandler.Invoke();

				if (operationSuccess == false)
					return;
				
				Flush();
				_window.SetActive(false);
				_warningWindow.SetActive(false);
			});
		}

		public void ChangeOnButtonClicked(Func<bool> onButtonClicked)
		{
			_onButtonClickedHandler = onButtonClicked;
		}

		private void Flush()
		{
			_onButtonClickedHandler = null;
		}
	}
}