using System;

namespace CarSumo.DataModel.Accounts
{
	public interface IAccountStorageMessages
	{
		IObservable<Account> ObserveAnyAccountValueChanged();
	}
}