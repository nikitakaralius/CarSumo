using System;
using CarSumo.Teams;
using GameModes;
using GuiBaseData.Accounts;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Accounts
{
	public class UiAccountPresenter : AccountView
	{
		[SerializeField] private Team _team;

		private IGameModePreferences _gameModePreferences;
		private IDisposable _accountSubscription;

		[Inject]
		private void Construct(IGameModePreferences preferences)
		{
			_gameModePreferences = preferences;
		}
		
		private void OnEnable()
		{
			_accountSubscription = _gameModePreferences
				.GetAccountByTeam(_team)
				.Subscribe(ChangeAccount);
		}

		private void OnDisable()
		{
			_accountSubscription?.Dispose();
		}
	}
}