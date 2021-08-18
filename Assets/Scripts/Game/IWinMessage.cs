using System;
using CarSumo.DataModel.Accounts;

namespace Game
 {
	public interface IWinMessage
	{
		IObservable<Account> ObserveWin();
	}
}