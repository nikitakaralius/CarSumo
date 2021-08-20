using System;
using System.Collections.Generic;
using AdvancedAudioSystem;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Menu.Accounts
{
	public class AccountChangerButton : SerializedMonoBehaviour
	{
		public enum ButtonTitle
		{
			NewAccount,
			ChangeAccount
		}

		[Header("Required Components")] [SerializeField]
		private Button _button;

		[SerializeField] private TMP_Text _text;
		[SerializeField] private GameObject _window;

		[Header("Titles")] [SerializeField] private IReadOnlyDictionary<ButtonTitle, string> _buttonTitles;

		private Func<bool> _onButtonClickedHandler;
		private IAudioPlayer _audioPlayer;

		[Inject]
		private void Construct(IAudioPlayer audioPlayer)
		{
			_audioPlayer = audioPlayer;
		}

		private void Start()
		{
			_button.onClick.AddListener(() =>
			{
				_audioPlayer.Play();
				bool operationSuccess = _onButtonClickedHandler.Invoke();

				if (operationSuccess)
					_window.SetActive(false);
			});
		}

		public void ChangeOnButtonClickedSubscription(Func<bool> onButtonClicked, ButtonTitle buttonTitle)
		{
			_onButtonClickedHandler = onButtonClicked;
			_text.text = _buttonTitles[buttonTitle];
		}
	}
}