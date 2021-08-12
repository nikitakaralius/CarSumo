using System;
using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;
using UniRx;

namespace GameModes
{
	public class GameModeRegistry : IGameModePreferences, IGameModeOperations
	{
		private readonly Dictionary<Team, ReactiveProperty<Account>> _registeredAccounts;

		public GameModeRegistry(IReadOnlyDictionary<Team, Account> initialRegisteredAccounts)
		{
			_registeredAccounts = initialRegisteredAccounts
				.ToDictionary(x => x.Key, x => new ReactiveProperty<Account>(x.Value));
		}
		
		public float TimerTimeAmount { get; private set; }
		
		public IReadOnlyReactiveProperty<Account> GetAccountByTeam(Team team)
		{
			if (_registeredAccounts.TryGetValue(team, out var account) == false)
			{
				throw new InvalidOperationException($"Can not find account with team {team}. Make sure it registered");
			}

			return account;
		}

		public void RegisterAccount(Team team, Account account)
		{
			if (_registeredAccounts.TryGetValue(team, out var registeredAccount) == false)
			{
				registeredAccount.Value = account;
			}
			else
			{
				_registeredAccounts[team] = new ReactiveProperty<Account>(account);
			}
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