using System.Collections.Generic;
using CarSumo.DataModel.Accounts;
using CarSumo.Teams;

namespace Menu.GameModes.OneDevice.Accounts
{
	public interface IAccountsPreferences
	{
		IReadOnlyDictionary<Team, Account> AccountsToRegister { get; }
	}
}