using System;
using CarSumo.DataModel.Accounts;
using Game;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Endgame.Win
{
	public class WinWindow : MonoBehaviour
	{
		[SerializeField] private WinnerAccountPresenter _winnerAccountPresenter;
		[SerializeField] private GameObject _window;
		
		private IEndGameMessage _endGameMessage;
		private IDisposable _winSubscription;
		
		[Inject]
		private void Construct(IEndGameMessage endGameMessage)
		{
			_endGameMessage = endGameMessage;
		}

		private void OnEnable()
		{
			_winSubscription = _endGameMessage
				.ObserveEnding()
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