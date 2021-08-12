using System.Collections.Generic;
using CarSumo.DataModel.Accounts;

namespace Menu.Accounts
{
	public interface IAccountListRules
	{
		bool SelectActivated { get; }
		void OnItemListCreated(AccountListItem item);
		IEnumerable<Account> AccountsToRender { get; }
	}
}