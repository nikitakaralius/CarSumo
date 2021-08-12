using System;
using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace GameModes
{
	public class GameModeRegistry : IGameModePreferences, IGameModeOperations
	{
		private readonly Dictionary<Team, Account> _accounts;

		public GameModeRegistry()
		{
			_accounts = new Dictionary<Team, Account>();
		}
		
		public float TimerTimeAmount { get; private set; }
		
		public Account GetAccountByTeam(Team team)
		{
			if (_accounts.TryGetValue(team, out Account account) == false)
			{
				throw new InvalidOperationException($"Can not find account with team {team}. Make sure it registered");
			}

			return account;
		}

		public void RegisterAccount(Account account, Team team)
		{
			if (account is null)
			{
				throw new InvalidOperationException("Account can not be null");
			}
			
			_accounts[team] = account;
		}

		public void ConfigureTimer(float timeAmount)
		{
			if (timeAmount < 0)
			{
				throw new InvalidOperationException("Trying to set negative amount of time");
			}

			TimerTimeAmount = timeAmount;
		}
	}
}