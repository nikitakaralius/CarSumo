using System.Collections.Generic;

namespace Menu.Accounts
{
	public interface IAccountListObserver
	{
		void OnAccountCreated(AccountListItem listItem);
		void OnAllItemsCreated(IEnumerable<AccountListItem> items);
	}
}