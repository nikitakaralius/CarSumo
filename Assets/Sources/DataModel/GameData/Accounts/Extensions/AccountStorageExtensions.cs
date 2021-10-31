using System.Collections.Generic;
using System.Linq;
using CarSumo.DataModel.Accounts;

namespace DataModel.GameData.Accounts.Extensions
{
	public static class AccountStorageExtensions
	{
		public static IEnumerable<Account> GetAccountsExceptActive(this IAccountStorage accountStorage)
		{
			Account activeAccount = accountStorage.ActiveAccount.Value;

			return accountStorage.AllAccounts
				.Where(account => account.Equals(activeAccount) == false);
		}
	}
}