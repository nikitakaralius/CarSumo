using System;
using CarSumo.DataModel.Accounts;

namespace Game
 {
	public interface IEndGameMessage
	{
		IObservable<Account> ObserveEnding();
	}
}