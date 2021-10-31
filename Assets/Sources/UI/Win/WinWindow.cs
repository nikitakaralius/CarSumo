using System;
using CarSumo.DataModel.Accounts;
using Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
	public class WinWindow : MonoBehaviour
	{
		[SerializeField] private WinnerAccountPresenter _winnerAccountPresenter;
		[SerializeField] private GameObject _window;
		
		private IWinMessage _winMessage;
		private IDisposable _winSubscription;
		
		[Inject]
		private void Construct(IWinMessage winMessage)
		{
			_winMessage = winMessage;
		}

		private void OnEnable()
		{
			_winSubscription = _winMessage
				.ObserveWin()
				.Subscribe(OnPlayerWon);
		}

		private void OnDisable()
		{
			_winSubscription.Dispose();
		}

		private void OnPlayerWon(Account winner)
		{
			_window.SetActive(true);
			_winnerAccountPresenter.ChangeAccount(winner);
		}
	}
}