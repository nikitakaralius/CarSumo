using System.Collections.Generic;

namespace CarSumo.DataModel.Accounts
{
	public interface IAccountRepository
	{
		IEnumerable<Account> Accounts { get; }
	}
}