using System.Collections.Generic;
using CarSumo.DataModel.Accounts;

namespace Menu.Accounts
{
	public interface IAccountListRules
	{
		bool SelectActivated { get; }
		void OnListItemCreated(AccountListItem item);
		IEnumerable<Account> AccountsToRender { get; }
	}
}