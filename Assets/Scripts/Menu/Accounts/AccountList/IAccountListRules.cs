using System.Collections.Generic;
using CarSumo.DataModel.Accounts;

namespace Menu.Accounts
{
	public interface IAccountListRules
	{
		IEnumerable<Account> AccountsToRender { get; }
	}
}